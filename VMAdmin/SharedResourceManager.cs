using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Net;

namespace VMAdmin
{
    public class SharedResourceManager : IDisposable
    {
        private readonly Dictionary<string, NetworkConnection> _connections = new();
        private readonly Dictionary<string, NetworkCredential> _credentialCache = new();

        public void Connect(string uncPath, string username, string password, bool cacheCredentials = true)
        {
            // Verificar conexión existente
            if (_connections.TryGetValue(uncPath, out var existingConn) && IsConnectionActive(existingConn))
                return;

            // Usar credenciales en caché si disponibles
            if (cacheCredentials && _credentialCache.TryGetValue(uncPath, out var cachedCred))
            {
                username = cachedCred.UserName;
                password = cachedCred.Password;
            }

            try
            {
                var credential = new NetworkCredential(username, password);
                var newConn = new NetworkConnection(uncPath, credential);

                _connections[uncPath] = newConn;

                if (cacheCredentials)
                    _credentialCache[uncPath] = credential;
            }
            catch (Win32Exception ex) when (ex.NativeErrorCode == 1219) // ERROR_SESSION_CREDENTIAL_CONFLICT
            {
                // Reconexión automática en caso de conflicto
                Disconnect(uncPath);
                Connect(uncPath, username, password, cacheCredentials);
            }
        }

        public void Disconnect(string uncPath)
        {
            if (_connections.TryGetValue(uncPath, out var conn))
            {
                conn.Dispose();
                _connections.Remove(uncPath);
            }
        }

        public void OpenResource(string uncPath)
        {
            if (_connections.ContainsKey(uncPath))
                Process.Start("explorer.exe", uncPath);
        }

        public void Dispose()
        {
            foreach (var conn in _connections.Values)
                conn.Dispose();

            _connections.Clear();
            _credentialCache.Clear();
            GC.SuppressFinalize(this);
        }

        public bool IsConnected(string uncPath) =>
            _connections.TryGetValue(uncPath, out var conn) && IsConnectionActive(conn);

        private bool IsConnectionActive(NetworkConnection conn)
        {
            try
            {
                // Alternativa segura para verificar conexión sin depender de NetworkName
                // Intenta listar el directorio raíz (esto varía según el recurso compartido)
                string testPath = conn.GetType().GetField("_networkName",
                    System.Reflection.BindingFlags.NonPublic |
                    System.Reflection.BindingFlags.Instance)?.GetValue(conn) as string;

                return !string.IsNullOrEmpty(testPath) && Directory.Exists(testPath);
            }
            catch
            {
                return false;
            }
        }
    }
}
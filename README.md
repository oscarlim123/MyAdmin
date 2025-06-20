# VMAdmin

**VMAdmin** es una aplicación de escritorio en C# para la administración remota de estaciones de trabajo en redes corporativas. Permite escanear rangos de IPs, copiar archivos a múltiples estaciones, descomprimirlos remotamente y realizar tareas automatizadas, todo con una interfaz intuitiva y adaptable.

---

## 🚀 Características principales

- 🔍 **Escaneo de rangos de IPs** para detectar estaciones activas
- 📁 **Carga y gestión de rangos personalizados**
- 🔐 **Conexión remota con credenciales locales o de dominio**
- 📦 **Copia de archivos (ZIP/RAR) a estaciones seleccionadas**
- 🗂️ **Descompresión remota automática** vía PowerShell o 7-Zip
- 🛠️ **Soporte para WinRM y PsExec**
- 📝 **Logs detallados por estación y por tarea**
- 🎯 **Interfaz minimalista y optimizada para administradores de red**

---

## 📸 Capturas de pantalla

> *(Aquí puedes insertar un par de imágenes si gustas)*

---

## ⚙️ Requisitos

- Windows 10/11 o Windows Server 2012+  
- PowerShell 4.0 o superior en estaciones remotas  
- WinRM habilitado o uso de `PsExec`  
- .NET Framework 4.8  
- 7za.exe (incluido) para descompresión portátil  

---

## 🛠️ Instalación

1. Clona este repositorio:  
   `git clone https://github.com/tu_usuario/VMAdmin.git`

2. Abre el proyecto en Visual Studio 2022 o superior

3. Asegúrate de incluir:
   - `7za.exe` en la carpeta raíz
   - `psexec.exe` si usarás ese método

4. Compila y ejecuta

---

## 🤖 Uso básico

1. Escanea o selecciona un rango de IPs
2. Carga un archivo `.zip` a copiar
3. Selecciona las estaciones
4. Inicia la carga y descompresión remota
5. Revisa logs por cada estación

---

## 🧠 ¿Para quién está pensada esta app?

Para administradores de red, técnicos de soporte, personal de infraestructura o cualquier profesional que necesite **implementar tareas masivas** en redes LAN con **más de una decena de equipos**.

---

## 📌 Licencia

MIT License — libre de usar, adaptar y extender.

---

## 🙌 Autor

**Oscar** — Analista de Infraestructura y automatización de redes  
Contacto: *(opcional)*  

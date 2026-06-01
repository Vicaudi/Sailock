using System.Collections.Generic;
using Sailock.Models;

namespace Sailock.Services
{
    public static class ChangelogService
    {
        public static List<ChangelogEntry> GetEntries() => new List<ChangelogEntry>
        {
            new ChangelogEntry
            {
                Version  = "1.2.0",
                Date     = "2026-06-01",
                IsLatest = true,
                Added = new List<string>
                {
                    "Añadido soporte para los idiomas español e inglés",
                    "La selección de idioma ya está disponible en Ajustes y se guarda automáticamente",
                    "Todas las vistas, diálogos y mensajes del sistema han sido traducidos"
                },
                Changed = new List<string>
                {
                    "Mejorada la apariencia de las barras de desplazamiento en el Registro de cambios",
                    "Actualizado el estilo del botón de versión para una mayor coherencia visual"
                },
                Fixed = new List<string>
                {
                    "Corregido un problema que podía impedir que algunos elementos de la interfaz se cargaran correctamente",
                    "Mejorado el sistema de traducciones para garantizar la carga correcta de todos los idiomas"
                }
            },
            new ChangelogEntry
            {
                Version = "1.1.3",
                Date    = "2026-05-30",
                IsLatest = false,
                Added   = new List<string>
                {
                    "Auto-Lock de sesión funcional: cierre automático tras 5 minutos de inactividad",
                    "Ventana de historial de versiones accesible desde el número de versión",
                    "Sistema de versiones con Semantic Versioning (Major.Minor.Patch)",
                },
                Fixed   = new List<string>
                {
                    "Modal Add New ya no permanece abierto al navegar entre vistas",
                    "El tema claro/oscuro ahora persiste correctamente al reiniciar la aplicación",
                    "El tamaño de interfaz persiste y tiene diferencia visible entre las opciones",
                    "El contraste visual persiste correctamente al reiniciar",
                    "Logo duplicado en la barra lateral eliminado",
                    "Placeholder del campo TOTP ahora aparece centrado",
                }
            },
            new ChangelogEntry
            {
                Version = "1.1.2",
                Date    = "2026-05-28",
                Added   = new List<string>
                {
                    "Import de archivo .slock externo con validación de contraseña maestra",
                    "Export de datos cifrados a archivo .slock como copia de seguridad",
                    "Delete All Data con confirmación y reseteo de fábrica",
                    "Mensaje de estado en Settings tras operaciones de Import/Export",
                },
                Fixed   = new List<string>
                {
                    "Import ahora recarga el Dashboard correctamente tras importar",
                    "Export genera un archivo .slock válido en todos los casos",
                }
            },
            new ChangelogEntry
            {
                Version = "1.1.1",
                Date    = "2026-05-27",
                Added   = new List<string>
                {
                    "Pantalla Settings completa: Security, Accessibility, Data Management, Danger Zone",
                    "2FA con TOTP compatible con Google Authenticator, Authy y cualquier app TOTP",
                    "Modal de confirmación al desactivar 2FA",
                    "Verificación de contraseña maestra antes de editar una entrada",
                    "Campo de contraseña con toggle mostrar/ocultar (icono de ojo)",
                    "Contraste visual configurable: bordes marcados o sutiles",
                    "Tamaño de interfaz configurable: Small, Default, Large",
                    "Modo claro con cambio de logo automático",
                },
                Fixed   = new List<string>
                {
                    "Almacenamiento: entradas cifradas con AES-256 en archivo .slock",
                    "Login conectado a contraseña maestra real",
                    "Primera ejecución guiada para crear la contraseña maestra",
                }
            },
            new ChangelogEntry
            {
                Version = "1.0.2",
                Date    = "2026-05-25",
                Added   = new List<string>
                {
                    "Dashboard funcional: lista de contraseñas, búsqueda y filtrado en tiempo real",
                    "Modal Add New con campos: título, categoría, email, usuario, contraseña, nota",
                    "Modal Edit con campos prellenados y opción de borrar entrada",
                    "Generador de contraseñas: longitud, charset, exclusiones y prefijo",
                    "Sidebar con navegación entre Dashboard, Generator y Settings",
                    "Botones personalizados de ventana: minimizar, maximizar, cerrar",
                    "Icono de aplicación (.ico) en ejecutable y barra de tareas",
                },
                Fixed   = new List<string>
                {
                    "Estilos globales unificados en App.xaml",
                    "RelayCommand corregido con CommandManager.RequerySuggested",
                }
            },
            new ChangelogEntry
            {
                Version = "1.0.1",
                Date    = "2026-05-25",
                Added   = new List<string>
                {
                    "Dashboard con creación y borrado de entradas funcional",
                    "Generador de contraseñas funcional con criterios configurables",
                },
                Fixed   = new List<string>
                {
                    "Login: validación de contraseña maestra real en lugar de valor fijo",
                    "Correcciones de navegación entre vistas",
                }
            },
            new ChangelogEntry
            {
                Version = "1.0.0",
                Date    = "2026-05-23",
                Added   = new List<string>
                {
                    "Estructura inicial del proyecto con arquitectura MVVM",
                    "Pantalla de inicio de sesión con contraseña provisional",
                    "Navegación entre vistas con ContentControl y DataTemplates",
                    "Fuente personalizada Supermolot Light integrada",
                    "Logo Sailock en login y barra lateral",
                }
            },
        };
    }
}
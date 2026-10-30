# GitHub Actions con .NET 10

## 1. Descripción del proyecto

Proyecto de capacitación orientado a implementar un pipeline de integración continua (CI) utilizando GitHub Actions para una aplicación desarrollada con .NET 10.

El objetivo es automatizar la restauración de dependencias, compilación, ejecución de pruebas y publicación de una API REST, utilizando workflows definidos mediante archivos YAML.

## 2. Tecnologías utilizadas

- C#
- .NET 10
- ASP.NET Core Web API
- xUnit para pruebas unitarias
- Git y GitHub
- GitHub Actions
- YAML
- GitHub-hosted runners (Ubuntu)

## 3. Estructura del proyecto

```text
GitHubActionsNet10/
├── .github/
│   └── workflows/
│       └── ci.yml
├── docs/
│   └── evidencias/
├── GitHubActionsNet10.Api/
├── GitHubActionsNet10.Tests/
├── .gitignore
├── GitHubActionsNet10.sln
└── README.md
```

## 4. Ejecución local

### Requisitos previos

- Git instalado.
- SDK de .NET 10.
- Visual Studio 2026 o un editor compatible con .NET 10.

### Restaurar dependencias

```bash
dotnet restore
```

### Compilar la solución

```bash
dotnet build
```

### Ejecutar las pruebas

```bash
dotnet test
```

### Ejecutar la API

```bash
dotnet run --project ./GitHubActionsNet10.Api/GitHubActionsNet10.Api.csproj --urls http://localhost:5000
```

Ejemplo de consulta:

```text
http://localhost:5000/api/calculadora/sumar?numero1=10&numero2=20
```

## 5. Pipeline de GitHub Actions

El workflow se encuentra en:

```text
.github/workflows/ci.yml
```

Su nombre es `CI - GitHubActionsNet10`.

### Eventos que activan el workflow

- `push`: ejecuta el workflow cuando se suben cambios al repositorio.
- `pull_request`: ejecuta el workflow ante eventos de Pull Request.
- `workflow_dispatch`: permite iniciar el workflow manualmente desde GitHub.

### Jobs implementados

**Job 1: Build and Test**

Realiza las siguientes tareas:

1. Descarga el código del repositorio.
2. Configura el SDK de .NET 10.
3. Restaura las dependencias.
4. Compila la solución en configuración Release.
5. Ejecuta las pruebas automatizadas.
6. Verifica la disponibilidad de la variable y el secret de prueba.

**Job 2: Publish API**

Realiza las siguientes tareas:

1. Descarga el código del repositorio.
2. Configura el SDK de .NET 10.
3. Restaura las dependencias.
4. Publica la API en configuración Release.
5. Sube el resultado como artifact de GitHub Actions.

Este job utiliza `needs: build-and-test`, por lo que depende de que el primer job finalice correctamente.

## 6. Runner utilizado

Se utiliza:

```yaml
runs-on: ubuntu-latest
```

Esto permite ejecutar los jobs en máquinas virtuales administradas por GitHub, sin necesidad de configurar un servidor propio para la ejecución del pipeline.

## 7. Variables y secrets

Se configuraron los siguientes valores a nivel del repositorio:

| Tipo | Nombre | Uso |
|---|---|---|
| Variable | `APP_ENVIRONMENT` | Identifica el entorno de capacitación. |
| Secret | `DEMO_API_TOKEN` | Valor ficticio utilizado para practicar el manejo seguro de información sensible. |

La variable se consume mediante:

```yaml
${{ vars.APP_ENVIRONMENT }}
```

El secret se consume mediante:

```yaml
${{ secrets.DEMO_API_TOKEN }}
```

El secret se utiliza únicamente en el step de verificación y no se imprime su contenido en los logs.

## 8. Artifact de publicación

El workflow utiliza `actions/upload-artifact` para almacenar el resultado de `dotnet publish`.

Nombre del artifact:

```text
GitHubActionsNet10-API
```

El artifact contiene los archivos generados durante la publicación de la API y puede descargarse desde el resumen de la ejecución correspondiente en GitHub Actions.

## 9. Troubleshooting: fallo controlado

Se realizó una prueba intencional de fallo utilizando el siguiente comando:

```bash
exit 1
```

El comando devuelve un código de salida distinto de cero, provocando que el step y el job `Build and Test` fallen.

### Resultado observado

- `Build and Test`: Failure.
- `Publish API`: Skipped.

El segundo job no se ejecutó debido a la dependencia configurada mediante `needs: build-and-test`.

### Corrección

Se deshabilitó el step de simulación mediante comentarios en el archivo YAML.

Posteriormente se realizó una nueva ejecución y se verificó que ambos jobs finalizaran correctamente.

El bloque comentado se conserva como referencia del ejercicio.

## 10. Equivalencias entre Jenkins y GitHub Actions

| Jenkins | GitHub Actions |
|---|---|
| Jenkinsfile | Workflow YAML |
| Pipeline | Workflow |
| Stage | Job (equivalencia aproximada) |
| Steps | Steps |
| Agent | Runner |
| Credentials | Secrets |
| Environment variables | Variables / env |
| Artifacts | Artifacts |
| Build triggers | Events (`on`) |
| Shared Libraries | Reusable Workflows / Actions |

## 11. Evidencias

Las capturas de las ejecuciones y pruebas realizadas se almacenarán en:

```text
docs/evidencias/
```

Se incluirán evidencias de:

- Ejecución exitosa del pipeline.
- Compilación y pruebas.
- Publicación de la API y generación del artifact.
- Uso de variables y secrets.
- Fallo controlado y análisis de logs.
- Corrección y recuperación del pipeline.

## 12. Resultado esperado

Contar con un pipeline funcional de integración continua para una solución .NET 10, que permita automatizar la compilación, ejecución de pruebas y publicación de la API, incorporando manejo de variables, secrets, artifacts y prácticas básicas de troubleshooting.


pipeline {
  agent any

  stages {
    stage('Restore') {
      steps { bat 'dotnet restore HelloApi.sln' }
    }

    stage('Build') {
      steps { bat 'dotnet build HelloApi.sln -c Release --no-restore' }
    }

    stage('Test') {
      steps { bat 'dotnet test HelloApi.sln -c Release --no-build' }
    }

   stage('Publish') {
  steps {
    bat '''
      if exist out rmdir /s /q out
      dotnet publish HelloApi.csproj -c Release -o out
    '''
  }
}

    stage('Deploy to Localhost') {
  steps {
    bat '''
      echo === STOP OLD APP (if running) ===
      powershell -NoProfile -ExecutionPolicy Bypass -Command ^
        "Get-Process dotnet -ErrorAction SilentlyContinue | Where-Object { $_.Path -like '*HelloApi.dll*' } | Stop-Process -Force -ErrorAction SilentlyContinue"

      echo === COPY NEW BUILD ===
      if not exist C:\\deploy\\HelloApi mkdir C:\\deploy\\HelloApi
      xcopy /E /Y /I out\\* C:\\deploy\\HelloApi\\

      echo === START NEW APP ===
      start "" /B dotnet C:\\deploy\\HelloApi\\HelloApi.dll --urls "http://localhost:5000"

      echo Waiting 5s for app to boot...
      powershell -NoProfile -Command "Start-Sleep -Seconds 5"
    '''
  }
}


stage('Smoke Test') {
  steps {
    bat '''
      powershell -NoProfile -Command "Invoke-WebRequest http://localhost:5000/health -UseBasicParsing | Select-Object -Expand Content"
    '''
  }
}

    

    stage('Archive Artifact') {
      steps { archiveArtifacts artifacts: 'out/**', fingerprint: true }
    }
  }
}

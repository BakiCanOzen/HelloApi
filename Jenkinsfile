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
      echo === STOP OLD APP by PORT 5000 ===
      for /f "tokens=5" %%a in ('netstat -ano ^| findstr :5000 ^| findstr LISTENING') do (
        echo Killing PID %%a
        taskkill /F /PID %%a >nul 2>&1
      )

      echo === COPY NEW BUILD ===
      if not exist C:\\deploy\\HelloApi mkdir C:\\deploy\\HelloApi
      xcopy /E /Y /I out\\* C:\\deploy\\HelloApi\\

      echo === START NEW APP ===
      set JENKINS_NODE_COOKIE=dontKillMe
      start "" /B dotnet C:\\deploy\\HelloApi\\HelloApi.dll --urls "http://localhost:5000"

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

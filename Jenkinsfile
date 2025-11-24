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
      steps { bat 'dotnet publish HelloApi/HelloApi.csproj -c Release -o out' }
    }

    stage('Archive Artifact') {
      steps { archiveArtifacts artifacts: 'out/**', fingerprint: true }
    }
  }
}

pipeline {
    agent any
    
    stages {
        stage('Fetch Code') {
            steps {
                checkout scmGit(branches: [[name: '*/main']], extensions: [], userRemoteConfigs: [[url: 'https://github.com/sekkarin/ConsoleApp-with-sonarQ-jenkins.git']])
            }
        }
       stage('Build & Test in Docker') {
            steps {
                script {
                    sh """
                    docker run --rm -v "${env.WORKSPACE}:/app" -w /app mcr.microsoft.com/dotnet/sdk:9.0 sh -c '
                    cd ConsoleApp1 &&
                    dotnet add package xunit &&
                    dotnet add package Microsoft.NET.Test.Sdk &&
                    dotnet add package xunit.runner.visualstudio &&
                    dotnet restore &&
                    dotnet build --no-restore &&
                     dotnet test --collect "Code Coverage" --results-directory coverage
                    '
                    """
                }
            }
        }
        stage('Code Analysis') {
            environment {
                SCANNER_HOME = tool 'SonarScanner'  // Make sure 'SonarScanner' matches Jenkins tool name
                SONARQUBE_SERVER = 'sonatqube-server'      // Make sure 'SonarQube' matches configured server in Jenkins
            }

            steps {
                script {
                    withSonarQubeEnv('sonatqube-server') {
                        sh "cd ConsoleApp1 && ${SCANNER_HOME}/bin/sonar-scanner \
                            -Dsonar.projectKey=CS-calculator \
                            -Dsonar.sources=." \
                    }
                }
            }
        }
    }
}
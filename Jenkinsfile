pipeline {
    agent {
        docker {
            image 'mcr.microsoft.com/dotnet/sdk:7.0'  // Use official .NET SDK image
            args '--user root'  // Run as root to avoid permission issues
        }
    }
    
    stages {
        stage('Fetch Code') {
            steps {
                checkout scmGit(branches: [[name: '*/main']], extensions: [], userRemoteConfigs: [[url: 'https://github.com/sekkarin/ConsoleApp-with-sonarQ-jenkins.git']])
            }
        }
         stage('Restore Dependencies') {
            steps {
                script {
                    sh 'dotnet restore'
                }
            }
        }
        stage('Build') {
            steps {
                script {
                    sh 'dotnet build --no-restore'
                }
            }
        }
        stage('Run Tests') {
            steps {
                script {
                    sh 'cd ConsoleApp1'
                    sh 'dotnet-coverage collect "dotnet test" -f xml -o "coverage.xml"'
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
                        sh "${SCANNER_HOME}/bin/sonar-scanner \
                            -Dsonar.projectKey=CS-calculator \
                            -Dsonar.sources=ConsoleApp1"
                    }
                }
            }
        }
    }
}
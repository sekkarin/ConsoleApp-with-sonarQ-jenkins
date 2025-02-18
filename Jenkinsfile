pipeline {
    agent any
    
    stages {
        stage('Fetch Code') {
            steps {
                checkout scmGit(branches: [[name: '*/main']], extensions: [], userRemoteConfigs: [[url: 'https://github.com/sekkarin/ConsoleApp-with-sonarQ-jenkins.git']])
            }
        }
        stage('Code Analysis') {
            environment {
                scannerHome = tool 'scannerHome'
            }
            steps {
                script {
                    withSonarQubeEnv('sonatqube-server') {
                        sh "${scannerHome}/bin/sonar-scanner \
                            -Dsonar.projectKey=CS-calculator \
                            -Dsonar.sources=ConsoleApp1"
                    }
                }
            }
        }
    }
}
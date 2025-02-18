pipeline {
    agent any

    environment {
        ZAP_IMAGE = 'zaproxy/zap-stable'  // OWASP ZAP Docker image
        TARGET_URL = 'http://localhost:80'  // URL of the app you want to scan
        ZAP_PORT = '80'  // Port that ZAP will use
        ZAP_WAIT_TIME = '30'  // Wait for ZAP container to initialize
    }

    stages {
        stage('Fetch Code') {
            steps {
                checkout scmGit(branches: [[name: '*/dast']], extensions: [], userRemoteConfigs: [[url: 'https://github.com/sekkarin/ConsoleApp-with-sonarQ-jenkins.git']])
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
                            -Dsonar.sources=."
                    }
                }
            }
        }
        stage('Build & Test in Docker') {
            steps {
                script {
                    sh """
                    docker run --rm -v "${env.WORKSPACE}:/app" -w /app mcr.microsoft.com/dotnet/sdk:9.0 sh -c '
                    cd ConsoleApp1 &&
                    apt-get update && apt-get install -y libc6 libxml2 &&
                    dotnet add package xunit &&
                    dotnet add package Microsoft.NET.Test.Sdk &&
                    dotnet add package xunit.runner.visualstudio &&
                    dotnet tool install --global dotnet-coverage &&
                    export PATH="$PATH:/root/.dotnet/tools" &&
                    dotnet restore &&
                    dotnet build --no-incremental &&
                    dotnet-coverage collect "dotnet test" -f xml -o "coverage.xml"
                    '
                    """
                }
            }
        }
        stage('Scan security') {
            steps {
                script {
                    echo 'Running OWASP ZAP Docker container for security scan...'
                    sh 'docker compose up --build'

                    // Run ZAP in the background as a Docker container
                    sh '''
                        docker run -d --name zap -p 8081:8080 $ZAP_IMAGE
                        sleep $ZAP_WAIT_TIME  # Wait for ZAP to initialize
                        docker exec zap zap-baseline.py -t $TARGET_URL -g genhtml -o /zap/wrk/spider_report
                    '''

                    // Copy the ZAP report generated in the container to the Jenkins workspace
                    sh 'docker cp zap:/zap/wrk/spider_report .'
                }
            }
        }
        stage('Deploy') {
            steps {
                script {
                    sh 'echo "Deploy..........."'
                }
            }
        }
    }
}

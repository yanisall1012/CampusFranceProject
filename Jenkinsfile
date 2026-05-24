pipeline {

    agent any

    environment {
        DOTNET_CLI_HOME = "C:\\Temp"
        CHROMEDRIVER_PATH = "C:\\tools\\chromedriver.exe"
    }

    stages {

        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Restore') {
            steps {
                bat 'dotnet restore CampusFranceProject\\CampusFranceProject.csproj'
            }
        }

        stage('Build') {
            steps {
                bat 'dotnet build CampusFranceProject\\CampusFranceProject.csproj --configuration Release'
            }
        }

        stage('Test') {
            steps {
                bat '''
                    dotnet test CampusFranceProject\\CampusFranceProject.csproj ^
                    --configuration Release ^
                    --logger "trx;LogFileName=test-results.trx" ^
                    --results-directory TestResults
                '''
            }

            post {
                always {
                    mstest testResultsFile: '**\\TestResults\\*.trx'
                }
            }
        }
    }

    post {
        success {
            echo 'Pipeline terminé avec succès !'
        }

        failure {
            echo 'Le pipeline a échoué. Vérifier les logs.'
        }
    }
}
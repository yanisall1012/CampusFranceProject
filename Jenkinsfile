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
        bat 'dotnet restore'
    }
}

stage('Build') {
    steps {
        bat 'dotnet build --configuration Release'
    }
}

stage('Test') {
    steps {
        bat 'dotnet test --configuration Release'
    }
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
pipeline{
    agent any
    stages{
        stage('Git'){
            steps{
                echo "Checking code from repo..."

                bat "echo Building ${BRANCH_NAME}..."
                
                //Checkout code from the repository
                //checkout scm
            }
        }
        stage('SonarQube - Analysis'){
            steps{
                echo "SonarQube Analysis..."
            }
        }
        stage('SonarQube - Quality Gates'){
            steps{
                echo "Checking Quality Gates..."
            }
        }
        stage('Validations'){
            steps{
                echo "Performing Validations"   
            }
        }
        stage('Build'){
            steps{
                echo "Build Application..."

                //Build the application
                //bat "dotnet build"
            }
        }
        stage('Deployment'){
            steps{
                echo "Deploying Website..."
            }
        }
    }
}

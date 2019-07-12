pipeline{
    agent any
    stages{
        stage('Git'){
            steps{
                echo "Checking code from repo..."

                //bat "echo Building ${BRANCH_NAME}..."
                
                //Checkout code from the repository
                checkout scm
            }
        }
        stage('SonarQube - Analysis'){
            steps{
                echo "SonarQube Analysis..."

                script{
                    def scanner = tool 'SonarQube Server';
                    withSonarQubeEnv('SonarQube Server') {
                        sh "${scanner}bin/sonar-scanner"
                    }
                }  
            }
        }
        stage('SonarQube - Quality Gates'){
            steps{
                echo "Checking Quality Gates..."

                script{
                    timeout(time: 1, unit: 'MINUTES') { // Just in case something goes wrong, pipeline will be killed after a timeout
                        def qg = waitForQualityGate() // Reuse taskId previously collected by withSonarQubeEnv
                            if (qg.status != 'OK') {
                                error "Pipeline aborted due to quality gate failure: ${qg.status}"
                        }
                    }    
                }
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

                script{
                    def DOTNET = "\"C:\\Program Files\\dotnet\\dotnet\""

                    //Clean the project
                    bat "${DOTNET} clean \"${WORKSPACE}\\Overworld\\Test\\Test.csproj\""
                    //Similar command is:
                    //dotnet msbuild -restore -target:Clean

                    //Build the project
                    bat "${DOTNET} build \"${WORKSPACE}\\Overworld\\Test\\Test.csproj\""
                    //Similar command is:
                    //dotnet msbuild -restore -target:Build

                }
            }
        }
        stage('Deployment'){
            steps{
                echo "Deploying Website..."
            }
        }
    }
    post{
        always{
            deleteDir()
        }
        success{
            echo "Pipeline executed successfully!"
        }
        failure{
            echo "Pipeline failed to execute!"
        }
    }
}

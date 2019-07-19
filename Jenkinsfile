pipeline{
    agent any
    stages{
        stage('Git'){
            steps{
                echo "Checking code from repo..."

                //bat "echo Building ${BRANCH_NAME}..."
                
                //Print out all environment variables
                echo bat(returnStdout: true, script: 'set')  

                //Checkout code from the repository
                checkout scm
            }
        }
        stage('SonarQube - Analysis'){
            steps{
                echo "SonarQube Analysis..."
                /*
                script{
                    def scanner = tool 'SonarScanner';
                    withSonarQubeEnv('SonarQubeServer') {
                        bat "\"${scanner}\\bin\\sonar-scanner\""
                    }
                }  
                sleep 20
                */
            }
        }
        stage('SonarQube - Quality Gates'){
            steps{
                echo "Checking Quality Gates..."
                /*
                script{
                    timeout(time: 1, unit: 'MINUTES') { // Just in case something goes wrong, pipeline will be killed after a timeout
                        def qg = waitForQualityGate() // Reuse taskId previously collected by withSonarQubeEnv
                            if (qg.status != 'OK') {
                                error "Pipeline aborted due to quality gate failure: ${qg.status}"
                        }
                    }    
                }
                */
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

                    HOLDER = bat(returnStdout: true, script: "@git diff-tree --no-commit-id --name-only -r \"${env.GIT_COMMIT}\" ")

                    def MOD = HOLDER.split("\n");

                    println "Length is " + MOD.length

                    /*
                    def test = ["1", "2", "3"]
                    if(!test.contains("20")){
                        echo "It's not in the list"
                        test.add("100")
                    }
                    */

                    
                    def MODULE_LIST = ["Jenkinsfile"]

                    for (i = 0; i < MOD.length; i++) {
                        element = MOD[i]
                        echo element
                        
                        if(!MODULE_LIST.contains(element)){
                            echo "Got Here"
                            MODULE_LIST.add(element)
                        }
                    }
                    
                    for (j=0; j< MODULE_LIST.size(); j++){
                        echo MODULE_LIST[j]
                    }

                    //Clean the project
                    //bat "${DOTNET} clean \"${WORKSPACE}\\Overworld\\Test\\Test.csproj\""
                    //Similar command is:
                    //dotnet msbuild -restore -target:Clean

                    //Build the project
                    //bat "${DOTNET} build \"${WORKSPACE}\\Overworld\\Test\\Test.csproj\""
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

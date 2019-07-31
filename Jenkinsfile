pipeline{
    agent any
    environment{
        newFile=null
    }
    stages{
        stage('Git'){
            steps{
                echo "Checking code from repo..."

                script{
                    newFile = new File("C:\\Users\\6100476\\Desktop\\teste\\\"${JOB_BASE_NAME} - ${BUILD_DISPLAY_NAME}\".txt")
                    newFile.createNewFile()
                    newFile.write("Stage ${STAGE_NAME} - Begin")
                }

                //bat "echo Building ${BRANCH_NAME}..."
                echo "${STAGE_NAME}"
                //Print out all environment variables
                echo bat(returnStdout: true, script: 'set')  

                //Checkout code from the repository
                checkout scm
                script{
                    newFile.append("\r\nStage ${STAGE_NAME} - Successfull")
                }
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

                    HOLDER = bat(returnStdout: true, script: "@git diff-tree --no-commit-id --name-only -r \"${env.GIT_COMMIT}\" ")

                    def MOD = HOLDER.split("\n");
                    
                    def MODULE_LIST = []

                    for (i = 0; i < MOD.length; i++) {
                        element = MOD[i]
 
                        if(element.substring(0,7)=="Modules"){
                            module=element.split("/");
                            echo module[1]

                            if(!MODULE_LIST.contains(module[1])){
                                MODULE_LIST.add(module[1])
                            }
                        }
                    }
                    
                    echo "Gonna print final list now"
                    for (j=0; j< MODULE_LIST.size(); j++){
                        //echo MODULE_LIST[j]
                        bat "build_modules.bat ${MODULE_LIST[j]}"
                    }

                    //echo MSBUILD

                    //bat "build_modules.bat \"${MODULE_LIST}\""

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
            echo "Always"
            //deleteDir()
        }
        success{
            echo "Pipeline executed successfully!"
        }
        failure{
            echo "Pipeline failed to execute!"
        }
    }
}

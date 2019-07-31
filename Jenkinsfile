pipeline{
    agent any
    environment{
        newFile=null
        CURRENT_STAGE=null
    }
    stages{
        stage('Git'){
            steps{
                echo "Checking code from repo..."

                script{
                    CURRENT_STAGE="${STAGE_NAME}"
                    def fileName = "C:\\Users\\6100476\\Desktop\\teste\\${JOB_BASE_NAME} - ${BUILD_DISPLAY_NAME}.txt"
                    echo fileName
                    newFile = new File(fileName)
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
                script{
                    CURRENT_STAGE="${STAGE_NAME}"
                    newFile.append("\r\nStage ${STAGE_NAME} - Begin")
                    error "ergerregreg"
                }
                
                /*
                script{
                    def scanner = tool 'SonarScanner';
                    withSonarQubeEnv('SonarQubeServer') {
                        bat "\"${scanner}\\bin\\sonar-scanner\""
                    }
                }  
                sleep 20
                */
                script{
                    newFile.append("\r\nStage ${STAGE_NAME} - Successfull")
                }
            }
        }
        stage('SonarQube - Quality Gates'){
            steps{
                echo "Checking Quality Gates..."
                script{
                    CURRENT_STAGE="${STAGE_NAME}"
                    newFile.append("\r\nStage ${STAGE_NAME} - Begin")
                }
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
                script{
                    newFile.append("\r\nStage ${STAGE_NAME} - Successfull")
                }
            }
        }
        stage('Build'){
            steps{
                echo "Build Application..."

                script{
                    CURRENT_STAGE="${STAGE_NAME}"
                    newFile.append("\r\nStage ${STAGE_NAME} - Begin")

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

                    newFile.append("\r\nStage ${STAGE_NAME} - Successfull")
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
            script{
                newFile.append("\r\nPipeline failed at Stage ${CURRENT_STAGE}")   
            }
        }
    }
}

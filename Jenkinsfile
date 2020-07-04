pipeline {
  agent {
    docker {
      image 'docker.pkg.github.com/haplo-tech/build.docker.dotnetcore.3/dotnetcore3:13'
      registryUrl 'https://docker.pkg.github.com/'
      registryCredentialsId 'Haplo-Tech-GitHub'
    }
  }
  stages {
    stage('Restore') {
      steps {
        sh 'sed -i -e "s/{{BUILD_NUMBER}}/$BUILD_NUMBER/g" ./src/Haplo.Authorization.Auth0/Haplo.Authorization.Auth0.csproj'
        sh 'dotnet restore'
      }
    }
    stage('Build') {
      steps {
        sh 'dotnet build --no-restore --configuration Release'
      }
    }
    stage('Pack') {
      steps {
        sh 'dotnet pack --no-build --output ./ --configuration Release'
      }
    }
    stage('Push') {
      when {
        expression {
          GIT_BRANCH = 'origin/' + sh(returnStdout: true, script: 'git rev-parse --abbrev-ref HEAD').trim()
          return GIT_BRANCH == 'origin/master'
        }
      }
      steps {
        withCredentials(bindings: [[$class: 'UsernamePasswordMultiBinding', credentialsId:'Haplo-Tech-GitHub', usernameVariable: 'githubUsername', passwordVariable: 'githubToken']]) {
          sh 'mono /usr/local/bin/nuget.exe source Add -Name "GitHub" -Source "https://nuget.pkg.github.com/Haplo-tech/index.json" -UserName "Haplo-tech" -Password "$githubToken"'
          sh 'mono /usr/local/bin/nuget.exe push "Haplo.Authorization.Auth0.*.nupkg" -Source "GitHub"'
        }
      }
    }
  } 
  environment {
    HOME = '/tmp'
    DOTNET_CLI_TELEMETRY_OPTOUT = 1
  }
}

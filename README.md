<img align="right" src="https://github.com/gislersoft/euora/blob/master/euoralogo.png" width="30%" height="30%" style="text-align:center">

# Euora - Abandoned Planet

Euora is a free open source video game used to teach some basic game development using the Unity Engine, the colaborators of this project are basically students of the Universidad Autónoma de Occidente UAO in Cali Colombia.

## Story Plot 2

Is the future year 3145 A.D the Homo Sapiens has been vanished from five planets (Earth, Moon, Mars and Europa) in the solar system for unknown reasons, Only Earth 2 and it´s moon Euora are known as the latest Homo Sapiens Colonies. Earth 2 has lost contact with any Homo Sapiens in Euora (4 years ago),  Euora is at 2 years travel distance from Earth 2 a first mission was sent to the planet looking to solve the mistery but the contact was lost. Earth 2 has send one last mission with no return ticket of one man the Astronaut, Physicist and Soldier Commander Alexandre Bob Martinez Wang.

## Level 1 - The landing

Commander Alexandre is entering on his spaceship to the Euora atmosphere, after landing near to the last known Homo Sapiens base he will discover the solitude and dangers of Euora.

### Level 1 - Mission 1

The Commander needs to shutdown the Sentinel Robot that is in "Base defend mode", but the doors of the base are locked and the switch to shutdown the robot is inside the base, find a way to enter into the base without being killed by the Sentinel. Watch your oxigen and temperature levels.

## Developers Guide

This guide is to download the repository and colaborate with the game, there are some rules and steps that must be followed before you can commit a change.

### Setup

For Windows users:

> **IMPORTANT!!** : Please, before sending any change to the repo verify that you are using **EXACTLY OR COMPATIBLE** this Unity version: **``` 2021.3.5f1 Personal ```**

1. Download git https://git-scm.com/downloads
2. Create a public github account
3. Go to https://github.com/gislersoft/euora and fork the repository.
4. Create a local folder and verify you have **ENOUGH DISK SPACE**. Example: C:\Users\gislersoft\Documents\Unity\UAO\VideoJuegos3D\EuoraRepo
5. Run as administrator git bash (From step 1).
6. Go to the folder in git bash: ``` cd "C:\Users\gislersoft\Documents\Unity\UAO\VideoJuegos3D\EuoraRepo" ```
7. Clone the repository there: ``` git clone https://github.com/gislersoft/euora.git ```
8. Create a remote reference to your fork (Replace your github user in the command): ``` git remote add fork https://github.com/<YOUR GIT HUB USER HERE>/euora.git ```
9. Open Unity Hub and Add a new project and then select the cloned repository folder.
10. Wait while Unity setup the project for the first time this will take a while.
11. Double check in Unity the switch to Visible Meta Files in **Edit** → **Project Settings** → **Editor** → **Version Control Mode** → Visible Meta Files
12. Double check in Unity the switch to Force Text in **Edit** → **Project Settings** → **Editor** → **Asset Serialization Mode** → Force Text

### Final checks
 
 Verify with ```git remote -v``` that your repo pointer are correct your expected output should be:
 
  ``` 
 $ git remote -v
fork    https://github.com/<YOUR GITHUB USERNAME>/euora.git (fetch)
fork    https://github.com/<YOUR GITHUB USERNAME>/euora.git (push)
origin  https://github.com/gislersoft/euora.git (fetch)
origin  https://github.com/gislersoft/euora.git (push)
 ``` 

 If is your firstime using git then configure your basic information using this commands, please do this before send your first commit.

  ``` 
  git config --global user.email "you@example.com"
  git config --global user.name "Your Name"
  ``` 

### Useful commands

Here are some useful GIT commands with their respective explanation in spanish.

- ```git status``` // Verificar los cambios
- ```git remote -v``` // Verificar los apuntadores a los repositorios
- ```git add <file>``` // Agregar el archivo a el cambio
- ```git add .``` // Agregar todos los archivos (Usar con cautela)
- ```git rm <file>``` // Marcar como borrado en el cambio
- ```rm <file>``` //Borrar el archivo del sistema de archivos
- ```git commit -m "bla bla bla"``` // Crear el commit con un mensaje
- ```git push <apuntador> <branch>``` // Enviar los cambios al repositorio y branch apuntados
- ```git pull <apuntador> <branch>``` // Traer los cambios del repositorio y branch apuntados
- ```git remote add <apuntador> <url del repo>``` //Crea un nuevo apuntador para el repositorio dado
- ```git remote remove <apuntador>``` // Borrar el apuntador del repositorio

### Code of conduct (Mejores Prácticas)
- NO USAR ESPACIO EN LOS NOMBRES NADIE QUIERE TENER QUE AGREGAR DOBLE COMILLA EN gitbash
- No enviar PR super masivos (Muchos archivos en un solo PR) usar commits atomicos.
- Usar CamelCase para el nombramiento de variables, metodos, Clases, Texturas, Modelos y Carpetas. Ejemplo: ```MiCarpeta``` , ```miVariable```, ```miMetodo```, ```MiClase```, ```miTextura.png```, ```MiTextura.png```, ```MiModelo```, ```miModelo```.
- Leer los readme de cada carpeta
- Colocar los modelos en una carpeta agrupada.
- Agrupar de manera coherente archivos relacionados entre si cómo por ejemplo archivos relacionados a un mismo modelo.
- Evitar tocar las escenas si no necesito realmente tocarlas, en su lugar utilizar escenas de pruebas y colocarlas en la carpeta de escenas de prueba.
- Utilizar o español o inglés en los comentarios o el código, pero evitar mezclarlos de manera que sea dificil leer el código. Ejemplo de evitar esto: ```int ammo = 20; int municionRate = 10```.
- Los modelos deben estar terminados y haber pasado el proceso de calidad.
- Cumplir con el principio de la menor sorpresa y que las clases y metodos tengan sentido. (Clean Code)



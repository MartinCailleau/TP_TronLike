# TP_TronLike

Objectif : Créer un jeu "Tron Light Cycle"

-  contrôle d'une moto
	- Utiliser un character controller
	- Le point de rotation de la moto doit être à l'avant.
	- La moto ne doit pas pouvoir reculer.
- Faire suivre la camera
- Ajouter une trail
- Detecter la collision d'une moto dans une trail
- Explosion d'une moto (VFX + SFX)
- Multijoueur en ecran splitté 
- Boucle de jeu (respawn des joueurs)
- Sound design
- Scoring
- UI


# Step 1
## Presentation de la structure du gameObject player :

- Dans le player, il y a 3 empty objects :
    - RotationHelper, situé au millieu de la roue avant.
    - Trail, situé en retrait à l'arrière. Ce sera le point de départ du trailRenderer.
    - CamTarget, situé au-dessus du véhicule. Ce sera le point que la camera suivra.

    ![](https://i.imgur.com/H8tZMGQ.png)


- Exemple de la configuration du player. La sphère verte visible autour de la roue avant symbolise un character controller. Vous devez en ajouter un et l’ajuster.

![](https://i.imgur.com/3E107mz.png)

![](https://i.imgur.com/6ROAXGf.png)

# Step 2 Contrôle du vehicule
- Pour controller le vehicule, créez un script PlayerControl et Ajoutez-le au player.

    - Voici le script en question, avec quelques trous à compléter :

    ![](https://i.imgur.com/9DjYk6Q.png)
    
    - voici des conseils pour ce script :
        - Pour déplacer votre player utilisez la fonction Move : 
            - GetComponent<CharacterController>().Move(votre vecteur de                     mouvement calculé);
        - Pour faire tourner votre player utilisez la fonction :
            - RotateAround(le point de pivot passé en paramètre public, ....)	
        - Pour calculer votre vecteur de mouvement et la rotation, vous devez utiliser le système d'input : 
	Input.GetAxis(....)
        - Pour empecher la marche arrière vous pouvez utiliser la fonction Mathf.Clamp01(...)
        - Pour faire passer un vecteur de l'espace global à local utilisez ce genre de fonction : 
	        - transform.TransformVector(monVecteur);
        - Pour améliorer le controle vous pouvez le rendre pour smooth en utilisant des fonctions d'interpolation comme 
Vector3.Lerp(...) pour le déplacement et Mathf.Lerp(...) pour la rotation.


# Step 3 : Faire suivre la camera 
- Créer un script CameraFollow.cs.Ajoutez-le sur la camera.
    - Déclarer les variables suivantes :
	 ![](https://i.imgur.com/Kbb61fp.png)


    - Y ajouter le code suivant : 
    ![](https://i.imgur.com/V6YBbql.png)
     *n'oubliez pas de déclarer les variables.
    
    - Drag’n’drop l’objet CamTarget du player dans la propriété Target du composent CameraFollow
    
        ![](https://i.imgur.com/riTBmsC.png)

# Step 4 : Ajouter une trail
- Ajouter un composent trailRenderer à l'objet trail du player


![](https://i.imgur.com/cTciCSi.png)
- Quelques explications sur des parametres du trailRenderer : 
    - Reglez le time à Infinity, sinon la trail disparait au bout d'un certain temps.
    - Min Vertex Distance : un nouveau point est ajouté à la trail dès que l'objet à parcouru cette distance.
    - Materials : Vous pouvez creer Trail Material dans le projet pour personnaliser votre trail.
        - voici un exemple de trail material ![](https://i.imgur.com/CZaXd94.png)

- Detecter des colisions avec les trailRenderers.
Pour cette fonctionnalité, pas le choix il faut la coder.
Vous Allez donc créer un script **TrailCollider**

- Les étapes pour programmer ce script : 
    - 1ere étape : 
        - On doit créer une liste de points (Vector3).
        - On doit y ajouter un nouveau point dès que le player a parcouru une certaine distance.
            (vous pouvez utiliser la fonction Vector3.Distance(dernier point du tableau, position actuelle))
    
    - 2eme étape :
        - On doit créer un algorithme pour detecter les collisions (checkCollision).
        - Pour cela, on va lancer un linecast entre chaques points(segments) de la liste de points.
        - Si un linecast percute un collider, on verifie que c'est un player, et on lance une coroutine de destruction 
            (Mettez un Debug.Log("Collision detected") dans un premier temps).

    - 3eme étape : 
        - On lance la fonction checkCollision seulement tout les 100 millisecondes 
            grace à une condition et une variable timer dans la boucle Update.

    - 4eme étape : 
        - Créer la coroutine de destruction.
        - Instancier un VFX d'explosion.
        - Faire disparaitre le player en question. 


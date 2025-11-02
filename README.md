# TP1 Modélisation Géométrique

Les scripts sont dans : `Assets/Scripts`
Le projet n'a qu'une scène : `Assets/Scene/SampleScene.unity`  

Dans cette scène, il y a 5 **GameObjects** ayant chacun un **meshRenderer**, un **meshFilter** et un **script** générant la forme demandé dans la fonction `OnDrawGizmos`.  
Chaque script (sauf Plan) possède un booléen "**Show Vertices as Gizmos**" permettant de visualiser le placement des vertices. Cette fonctionnalité m'a aidé à visualiser mes problèmes pendant la construction des meshs. Je l’ai donc jugée utile à conserver dans l’éditeur.

> ⚠️ Tous les meshes sont visibles directement dans le viewport de l’éditeur : il n’est **pas nécessaire de lancer le mode "Play"**.

## Exercice 1 - Plan
 
Ce script / GameObject affiche deux triangles afin de former un plan.  
Les variables **Height** et **Width** permettent de régler respectivement la **hauteur** et la **largeur** de ce plan.

**Note** : Ce code est réutilisé dans les autres scripts via les fonctions `DrawTriangles` et `DrawPlan`, dessinant respectivement deux triangles et deux plans (un pour chaque face visible).
Pour inverser le "côté de caméra" lorsqu'on dessine un triangle, il suffit d'inverser l'ordre des vertices : 

```csharp
triangles.Add(a);
triangles.Add(b);
triangles.Add(c);
```
devient

```csharp
triangles.Add(c);
triangles.Add(b);
triangles.Add(a);
```


## Exercice 2 - Cylindre

Ce script / gameObject affiche un **cylindre** défini par les paramètres suivants : 
- **Nb Meridian** : nombre de méridiens
- **Rayon** : rayon de la base
- **Hauteur** : hauteur totale

Concrètement :  
On place les vertices en cercle de taille *Rayon* selon *Nb Meridian* à la hauteur 0 et à la hauteur *Hauteur*. Ces cercles sont reliés entre-eux par `DrawPlan` pour former la surface latérale.  
Enfin, chaque cercle est relié à un vertex central pour former les faces supérieure et inférieure du cylindre.

## Exercice 3 – Sphère

Ce script/GameObject affiche une **sphère** caractérisée par :  
- **Nb Meridian** : nombre de méridiens  
- **Nb Parallele** : nombre de parallèles  
- **Rayon** : rayon de la sphère

Concrètement :  
Deux vertices sont placés aux pôles nord et sud. Entre eux, plusieurs cercles horizontaux (parallèles) sont générés selon *Nb Parallele*.  
Sur chaque cercle, les vertices sont disposés selon *Nb Meridian*.  
Les pôles sont reliés aux premier et dernier cercles, et chaque cercle est relié au suivant via `DrawPlan`, formant ainsi la surface complète de la sphère.

## Exercice 4 – Cône tronqué

Ce script/GameObject affiche un **cône tronqué** défini par :  
- **Nb Meridian** : nombre de méridiens  
- **Rayon Base** : rayon de la base inférieure  
- **Hauteur** : hauteur totale  
- **Hauteur Tronquée** : hauteur à laquelle le cône est coupé

> **Note :** Une valeur de *Hauteur Tronquée* inférieure à 0 ou supérieure à *Hauteur* n’est pas prise en compte.

Concrètement :  
Deux cercles de vertices sont créés :
- le premier à la base (rayon *Rayon Base*, hauteur 0),
- le second au niveau de la coupe, avec un rayon réduit proportionnellement à *Hauteur Tronquée*.

Les cercles sont reliés entre eux via `DrawPlan` pour former la surface latérale.
Les vertices de chaque cercle sont ensuite reliés à un point central afin de fermer le cône tronqué sur ses deux faces.

> **Note :** Le dernier GameObject nommé *Cône* correspond à un test de dessin naïf reliant toutes les vertices de la base à une seule vertice au sommet.
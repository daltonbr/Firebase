# Firebase

Based on the course [Unity: Working with Unity Firebase](https://www.linkedin.com/learning/unity-working-with-google-firebase)

## Course details

1h 31m  Intermediate  Released: July 21, 2017

A dependable back-end is a must-have for today's games. Giving users the ability to log in and save and retrieve player data can make or break a community-oriented game. The goal of this course is to build a flexible and scalable networking solution, which can be reused in any of your future games, using Unity and Google Firebase. Instructor Harrison Ferrone starts with an overview of the Firebase Authentication and Realtime Database modules and the Firebase SDK. He shows how to script some basic functionality for logging in and registering users, and progresses to more complex functions like saving and fetching data, waiting for responses while providing good visual feedback, and displaying real-time data in the Unity GUI. By following along, you will understand how to integrate Firebase with Unity, save and fetch user data in a game scene, and bundle your work in asset packages that can be used in many other Unity projects.

## Details

[firebase.google.com](https://firebase.google.com)

Firebase is based on OAuth 2.0
In this course we are going to use Firebase for **authentication** and as a **Database as a Service** (DaaS) to hold a High Score Leader board.
This course is based initially for a iOS game deployment, but can be easily extended to Android or other platforms.

### Cocoapods and Gems

We need to set some Cocoapods in Xcode in order to use Firebase. So we may need to update our Gem installer for this.

## Important initial settings

* We need to set-up a Firebase account
* Download the Firebase SDK and extract the Unity Packages that we are going to use into our project (authentication and Database)
* Create our iOS (or other kind of) project inside Firebase
* Download our **Token** and put it somewhere into our *Assets* folder in Unity.
  * This file contains configuration details such as keys and identifiers, for the services you just enabled.
  * For iOS the file is a called **GoogleService-Info.plist**
* In Unity, in `Player Settings`
  * set the `Bundle Identifier` to the same name entered in the Firebase, in this case `com.daltonlima.firequest`
  * set the `Scripting Backend` to `.NET 2.0` and **NOT** the .Net 2.0 Subset

The Firebase [Documentation](https://firebase.google.com/docs) is awesome, and is our friend!

[Unity Reference](https://firebase.google.com/docs/reference/unity/)

## Database

`Firebase.Database.DataSnapshot` is just an iDictionary of key value pairs, corresponding to the JSON structure of our backend.

### Sort and filters

```csharp
    Router.Players().OrderByChild("level");
    Router.Players().LimitToFirst(10);
    // just one filter per query
    Router.Players().OrderByChild("level").LimitToLast(5);
```

### Different write methods

- `SetRawJsonValueAsync`
- `SetValueAsync`
- `Push`
- `UpdateChildrenAsync` (only update given nodes, without overriding other fields)
- `RunTransaction` (for concurrent updates)

## Tips for reusing code from this project

- We can create Unity AssetPackages, don't add the Firebase SDK, it's better to always get the latest version from their website.
    - For a starter pack, include the `Database`, the `AuthManager`, and the `Router`
- From your Firebase web console, download the appropriate files from your project, and change the project id URL inside `DatabaseManager.cs`
- Change the nodes names inside `Router.cs` to reflect your data structure

## Upgrades

- Database manager
    - replacing specific getPlayers method with a more generic get
- Router class
    - add convenience constructors for making life easier when you're using filters and queries
- Firebase docs have lots of another cool features

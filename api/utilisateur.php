<?php
require_once '../headers.php';
require_once '../database.php';

if ($_SERVER['REQUEST_METHOD'] === 'GET') {
    if (isset($_GET['id'])) {
        $id = $conn->real_escape_string($_GET['id']);
        $sql = $conn->query("SELECT * FROM film WHERE ident_film = '$id'");
        $data = $sql->fetch_assoc();
    } else { // fetch all rows
        $data = array();
        $sql = $conn->query("SELECT * FROM film");
        while ($d = $sql->fetch_assoc()) {
            $data[] = $d;
        }
    }
    exit(json_encode($data)); //return json
    // debug json
    // echo json_last_error();
}

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    try {
        $data = json_decode(file_get_contents("php://input"));
        // Récupération des données

        if (!empty($data->nom) && !empty($data->prenom) && !empty($data->mdp) && !empty($data->email)) {
            $nom = $data->nom;
            $prenom = $data->prenom;
            $mdp = $data->mdp;
            $email = $data->email;

            $database = new Database();

            // Hash du mot de passe
            $hashed_password = $database->HashPassword($mdp);

            $conn = $database->GetConnexion();

            // Requête préparée
            $query = "INSERT INTO utilisateur(mail, mdp, nom, prenom, verifie, id_type_compte) VALUES('$email', '$hashed_password', '$nom', '$prenom', 0, 4)";

            $statement = $conn->prepare($query);
            $statement->execute();

            exit("Création réussie");
            /* Récupération de toutes les lignes d'un jeu de résultats
            $resultArray = $statement->fetchAll();
            exit(json_encode($resultArray));*/
        } else {
            exit("Veuillez renseigner tous les champs");
        }
    } catch (Exception $e) {
        echo "error: ";
        exit($e);
    }
}

if ($_SERVER['REQUEST_METHOD'] === 'PUT') {
    echo 'PUT';
}

if ($_SERVER['REQUEST_METHOD'] === 'DELETE') {
    echo 'DELETE';
}

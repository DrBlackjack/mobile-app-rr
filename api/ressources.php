<?php
require_once '../headers.php';
require_once '../database.php';

if ($_SERVER['REQUEST_METHOD'] === 'GET') {
    $database = new Database();
    $conn = $database->GetConnexion();
    // Requête préparée
    $query = "SELECT * FROM ressources";

    $statement = $conn->prepare($query);
    $statement->execute();

    // Récupération de toutes les lignes d'un jeu de résultats
    $resultArray = $statement->fetchAll();
    exit(json_encode($resultArray));
}

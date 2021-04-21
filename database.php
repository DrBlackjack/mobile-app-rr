<?php
class Database
{
    function GetConnexion()
    {
        $server = "localhost";
        $username = "root";
        $password = "";
        $dbname = "ressources_relationnelles";

        // Création de la connexion
        try {
            $conn = new PDO("mysql:host=$server;dbname=$dbname", "$username", "$password");
            $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
        } catch (PDOException $e) {
            die('Erreur: Connexion impossible: ' . $e);
        }
        return $conn;
    }

    function HashPassword($password)
    {
        $options = [
            'memory_cost' => 1 << 17, // 128 Mb
            'time_cost'   => 4,
            'threads'     => 3,
        ];
        $hash = password_hash($password, PASSWORD_ARGON2I, $options);
        return $hash;
    }
}

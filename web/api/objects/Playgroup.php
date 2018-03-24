<?php
class Game{
 
    // database connection and table name
    private $conn;
    private $table_name = "Game";
 
    // object properties
    public $id;
    public $name;
    public $password;
    public $passwordSalt;
 
    // constructor with $db as database connection
    public function __construct($db){
        $this->conn = $db;
    }
}
?>
<?php
class Deck{
 
    // database connection and table name
    private $conn;
    private $table_name = "Deck";
 
    // object properties
    public $id;
    public $name;
    public $description;
    public $ownerId;
 
    public $owner;

    // constructor with $db as database connection
    public function __construct($db){
        $this->conn = $db;
    }
}
?>
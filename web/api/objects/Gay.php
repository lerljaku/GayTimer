<?php
class Gay{
 
    // database connection and table name
    private $conn;
    private $table_name = "Gay";
 
    // object properties
    public $id;
    public $firstName;
    public $lastName;
    public $nick;
    public $created;
 
    // constructor with $db as database connection
    public function __construct($db){
        $this->conn = $db;
    }
}
?>
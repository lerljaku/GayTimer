<?php
abstract class EntityBase{
 
    // database connection and table name
    private $conn;
    
    abstract protected function TableName();

    // constructor with $db as database connection
    protected function __construct($db){
        $this->conn = $db;
    }
        
    function readAll()
    { 
        // select all query
        $query = "SELECT * FROM " . TableName();
     
        // prepare query statement
        $stmt = $this->conn->prepare($query);
     
        // execute query
        $stmt->execute();
 
        return $stmt;
    }
}
?>
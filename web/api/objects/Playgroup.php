<?php
class Playgroup{
 
    // database connection and table name
    private $conn;
    private $table_name = "Playgroup";
 
    // object properties
    public $id;
    public $name;
    public $password;
    public $passwordSalt;
 
    // constructor with $db as database connection
    public function __construct($db)
    {
        $this->conn = $db;
    }
    
    function read()
    { 
        // select all query
        $query = "SELECT * FROM " . $this->table_name;
     
        // prepare query statement
        $stmt = $this->conn->prepare($query);
     
        // execute query
        $stmt->execute();
 
        return $stmt;
    }
    
    // create product
    function create(){
     
        // query to insert record
        $query = "INSERT INTO " . $this->table_name . " 
                  SET Name=:name, Password=:password, PasswordSalt=:passwordSalt";
     
        // prepare query
        $stmt = $this->conn->prepare($query);
     
        // sanitize
        $this->name = htmlspecialchars(strip_tags($this->name));
        $this->password = htmlspecialchars(strip_tags($this->password));
        $this->passwordSalt = htmlspecialchars(strip_tags($this->passwordSalt));
     
        // bind values
        $stmt->bindParam(":name", $this->name);
        $stmt->bindParam(":password", $this->password);
        $stmt->bindParam(":passwordSalt", $this->passwordSalt);
        
        // execute query
        if($stmt->execute()){
            return true;
        }
     
        return false;    
    }    
}
?>
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
    public $password;
    public $passwordSalt;
    public $created;
 
    // constructor with $db as database connection
    public function __construct($db){
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
       
    function create()
    { 
        // query to insert record
        $query = "INSERT INTO " . $this->table_name . " SET 
        firstName=:firstName, 
        lastName=:lastName, 
        nick=:nick, 
        password=:password, 
        passwordSalt=:passwordSalt, 
        created=:created";
 
        // prepare query
        $stmt = $this->conn->prepare($query);
 
        // sanitize
        $this->name=htmlspecialchars(strip_tags($this->firstName));
        $this->lastName=htmlspecialchars(strip_tags($this->lastName));
        $this->nick=htmlspecialchars(strip_tags($this->nick));
        $this->password=htmlspecialchars(strip_tags($this->password));
        $this->passwordSalt=htmlspecialchars(strip_tags($this->passwordSalt));
        $this->created=htmlspecialchars(strip_tags($this->created));
 
        // bind values
        $stmt->bindParam(":firstName", $this->firstName);
        $stmt->bindParam(":lastName", $this->lastName);
        $stmt->bindParam(":nick", $this->nick);
        $stmt->bindParam(":password", $this->password);
        $stmt->bindParam(":passwordSalt", $this->passwordSalt);
        $stmt->bindParam(":created", $this->created);
 
        // execute query
        if($stmt->execute()){
            return true;
        }
 
        return false;
     
    }
}
?>
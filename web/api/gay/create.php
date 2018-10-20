<?php
// required headers
header("Access-Control-Allow-Origin: *");
header("Content-Type: application/json; charset=UTF-8");
header("Access-Control-Allow-Methods: POST");
header("Access-Control-Max-Age: 3600");
header("Access-Control-Allow-Headers: Content-Type, Access-Control-Allow-Headers, Authorization, X-Requested-With");
 
// get database connection
include_once '../config/database.php';
 
// instantiate product object
include_once '../objects/Gay.php';
 
$database = new Database();
$db = $database->getConnection();
 
$user = new Gay($db);
 
// get posted data
$data = json_decode(file_get_contents("php://input"));
 
// set product property values
$user->nick = $data->Nick;
$user->passwordHash = $data->PasswordHash;
$user->passwordSalt = $data->PasswordSalt;
$user->created = $data->Created;
 
error_log($data->Nick);
error_log($data->PasswordHash);
error_log($data->PasswordSalt);
error_log($data->Created);

// create the product
if($user->create()){
    echo '{';
        echo '"message": "User was created."';
    echo '}';
}
 
// if unable to create the product, tell the user
else{
    echo '{';
        echo '"message": "Unable to create user."';
    echo '}';
}
?>
<?php
// required headers
header("Access-Control-Allow-Origin: *");
header("Content-Type: application/json; charset=UTF-8");
header("Access-Control-Allow-Methods: POST");
header("Access-Control-Max-Age: 3600");
header("Access-Control-Allow-Headers: Content-Type, Access-Control-Allow-Headers, Authorization, X-Requested-With");
 
// get database connection
include_once '../config/database.php';
 
// instantiate playgroup object
include_once '../objects/playgroup.php';
 
$database = new Database();
$db = $database->getConnection();
 
$playgroup = new Playgroup($db);
 
// get posted data
$data = json_decode(file_get_contents("php://input"));

$decryptedPwd = $data->password;
$options = [
    'cost' => 11,
    'salt' => mcrypt_create_iv(22, MCRYPT_DEV_URANDOM),
];
$encrypted = password_hash($decryptedPwd, PASSWORD_BCRYPT, $options);

// set playgroup property values
$playgroup->name = $data->name;
$playgroup->password = $data->password;
$playgroup->salt = $$options['salt'];

// create the playgroup
if($playgroup->create()){
    echo '{';
        echo '"message": "playgroup was created."';
    echo '}';
}
 
// if unable to create the playgroup, tell the user
else{
    echo '{';
        echo '"message": "Unable to create playgroup."';
    echo '}';
}
?>
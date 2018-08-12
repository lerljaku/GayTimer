<?php
// required headers
header("Access-Control-Allow-Origin: *");
header("Content-Type: application/json; charset=UTF-8");
 
// include database and object files
include_once '../config/database.php';
include_once '../objects/gay.php';
 
// instantiate database and gay object
$database = new Database();
$db = $database->getConnection();
 
// initialize object
$gay = new Gay($db);
 
// query gays
$stmt = $gay->read();
$num = $stmt->rowCount();
 
// check if more than 0 record found
if($num > 0){
 
    // gays array
    $gays_arr=array();
 
    // retrieve our table contents
    // fetch() is faster than fetchAll()
    // http://stackoverflow.com/questions/2770630/pdofetchall-vs-pdofetch-in-a-loop
    while ($row = $stmt->fetch(PDO::FETCH_ASSOC)){
        // extract row
        // this will make $row['name'] to
        // just $name only
        extract($row);
 
        $gay_item=array(
            "Id" => $Id,
            "FirstName" => $FirstName,
            "LastName" => $LastName,
            "Nick" => $Nick,
            "Password" => $Password,
            "PasswordSalt" => $PasswordSalt,
            "Created" => $Created,
        );
 
        array_push($gays_arr, $gay_item);
    }
 
    echo json_encode($gays_arr);
}
 
else{
    echo json_encode(
        array("message" => "No gays found.")
    );
}
?>
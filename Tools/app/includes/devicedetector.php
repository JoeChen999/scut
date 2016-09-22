<?php

class DeviceDetector
{
    public static $isOldIOSDevice  = false; 
    public static $isNewIOSDevice  = false;
    public static $isiPad4Device   = false;
    public static $isAndroidDevice = false;
    public static $isAmazonDevice  = false;

    public static $category        = "";

    public static function detect()
    {
        $agent = $_SERVER['HTTP_USER_AGENT'];
    
        if (strpos($agent, 'iPad') !== false) {
            if (strpos($agent, 'OS 4') !== false) {
                self::$isiPad4Device = true;
	    }else if(strpos($agent, 'OS 5') !== false) {
                self::$isiPad4Device = true;
	    }else if(strpos($agent, 'OS 6') !== false) {
                self::$isiPad4Device = true;
	    }else if(strpos($agent, 'OS 7') !== false) {
                self::$isiPad4Device = true;
            }else if(strpos($agent, 'OS 8') !== false) {
		self::$isiPad4Device = true;
	    }else if(strpos($agent, 'OS 9') !== false) {
                self::$isiPad4Device = true;
            }else {
                self::$isOldIOSDevice = true;
            }
        } else if (strpos($agent, 'iPhone') !== false) {
            if (strpos($agent, 'iPhone OS 4') !== false) {
                self::$isNewIOSDevice = true;
	        } else if (strpos($agent, 'iPhone OS 5') !== false) {
                self::$isNewIOSDevice = true;
	        } else if (strpos($agent, 'iPhone OS 6') !== false) {
                self::$isNewIOSDevice = true;
	        } else if (strpos($agent, 'iPhone OS 7') !== false) {
                self::$isNewIOSDevice = true;
	        } else if (strpos($agent, 'iPhone OS 8') !== false) {
		        self::$isNewIOSDevice = true;
            } else if (strpos($agent, 'iPhone OS 9') !== false) {
                self::$isNewIOSDevice = true;
            } else {
                self::$isOldIOSDevice = true;
            }
        } else if (strpos($agent, 'Android') !== false) {
            self::$isAndroidDevice = true;
        } else if (strpos($agent, 'U;') !== false) {
            self::$isAmazonDevice = true;
        }
    
        if (self::$isNewIOSDevice) {
            self::$category = "browser-ios4";
        } else if (self::$isiPad4Device) {
            self::$category = "browser-ipad4";
        } else if (self::$isOldIOSDevice) {
            self::$category = "browser-old-ios";
        } else if (self::$isAndroidDevice) {
            self::$category = "browser-android";
        } else if (self::$isAmazonDevice) {
            self::$category = "browser-amazon";
        } else {
            self::$category = "browser-desktop";
        }
    }
}


?>

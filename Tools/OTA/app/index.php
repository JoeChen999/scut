<?php
    require_once('config.php');
    require(constant('HOCKEY_INCLUDE_DIR'));
    
    $router = Router::get(array('appDirectory' => dirname(__FILE__).DIRECTORY_SEPARATOR));
    $apps = $router->app;
    

    $b = $router->baseURL;
    DeviceDetector::detect();
    //print_r($apps->applications);
    $ipaAdHoc=array();
    $ipaPayment=array();
    foreach ($apps->applications as $i => $app)
    {
        if ($app[AppUpdater::INDEX_PLATFORM] == AppUpdater::APP_PLATFORM_IOS)
        {
            if(preg_match("/Ent/",$app['app']))
            {
                array_push($ipaAdHoc,$app);
            }
            else
            {
                array_push($ipaPayment,$app);
            }
        }
    }
    $ipaAdHoc =  array_sort($ipaAdHoc,"date");
    $ipaPayment = array_sort($ipaPayment,"date");
    $apps->applications = array_sort($apps->applications,"date");
    
function array_sort($arr,$keys,$type='asc'){
    $keysvalue = $new_array = array();
    foreach ($arr as $k=>$v){
        $keysvalue[$k] = $v[$keys];
    }
    if($type == 'asc'){
        arsort($keysvalue);
    }else{
        arsort($keysvalue);
    }
    reset($keysvalue);
    foreach ($keysvalue as $k=>$v){
        $new_array[$k] = $arr[$k];
    }
    return $new_array;
}
?>
<!DOCTYPE html>
<html>
    <head>
        <meta charset="utf-8">
        <title>COK APPs</title>
        <meta name="viewport" content="width=device-width" />
        <link rel="stylesheet" href="<?php echo $b ?>blueprint/screen.css" type="text/css" media="screen, projection">
        <link rel="stylesheet" href="<?php echo $b ?>blueprint/print.css" type="text/css" media="print">
        <!--[if IE]><link rel="stylesheet" href="<?php echo $b ?>blueprint/ie.css" type="text/css" media="screen, projection"><![endif]-->
        <link rel="stylesheet" href="<?php echo $b ?>blueprint/plugins/buttons/screen.css" type="text/css" media="screen, projection">
        <link rel="stylesheet" type="text/css" href="<?php echo $b ?>css/stylesheet.css">
        <link rel="alternate" type="application/rss+xml" title="App Updates" href="<?php echo $b ?>feed.php" />
    </head>
    <body style="vertical-align:bottom; height:100%;background-color:#000000; background-image: url(../images/bg-2.gif); background-size:100%;background-repeat:no-repeat" class="<?php echo DeviceDetector::$category;?>>
        <div id="container" class="container">
            
            <?php if (DeviceDetector::$isAndroidDevice) { ?>
                <div class='android'>

                    <div style="margin-bottom:12px;">
                    	<img src="images/kabam_logo.png" alt="" style="width:100px;height:55px;vertical-align:middle;"/>
                    	<span style="margin-left:8px;font-size:14px;font-weight:bold;">SmokeBomb: App Installer</span>
                    </div>

                <?php
                    $androidAppsAvailable = 0;

                    foreach ($apps->applications as $i => $app) : 
                        if ($app[AppUpdater::INDEX_PLATFORM] == AppUpdater::APP_PLATFORM_ANDROID) {
                            $androidAppsAvailable++;
                        }
                    endforeach;

                    if ($androidAppsAvailable > 1) { 
                ?>
                    <p class="bordertop"></p>
                    <div class="grid">
                <?php
                        $column= 0;
                        foreach ($apps->applications as $i => $app) :
                            if ($app[AppUpdater::INDEX_PLATFORM] == AppUpdater::APP_PLATFORM_IOS)
                                continue;

                            $column++;
                ?>
                        <div class="column span-4">
                            <a href="#<?php echo $app[AppUpdater::INDEX_APP] ?>">
                <?php if ($app[AppUpdater::INDEX_IMAGE]) { ?>
                                <img class="icon" src="<?php echo $b.$app[AppUpdater::INDEX_IMAGE] ?>">
                <?php } ?>
                                <h4><?php echo $app[AppUpdater::INDEX_APP] ?></h4>
                            </a>
                        </div>

                <?php
                            if ($column == 2) {
                                echo "<div style='clear:both;'></div>";
                                $column = 0;
                            }
                        endforeach;
                ?>
                    </div>
                <?php
                    }
                ?>
                    <div style='clear:both;'><br/></div>

                <?php if ($androidAppsAvailable > 1) { ?>
                    <p><br/></p>
                <?php } ?>
                <?php
                    foreach ($apps->applications as $i => $app) : 
                        if ($app[AppUpdater::INDEX_PLATFORM] == AppUpdater::APP_PLATFORM_IOS)
                            continue;
                ?>

                    <div class="version">
                        <p class="borderbottom"></p>
                        <a name="<?php echo $app[AppUpdater::INDEX_APP] ?>"><br/></a>
                    <?php if ($app[AppUpdater::INDEX_IMAGE]) { ?>
                        <img class="icon" src="<?php echo $b.$app[AppUpdater::INDEX_IMAGE] ?>">
                    <?php } ?>
                        <h2><?php echo $app[AppUpdater::INDEX_APP] ?></h2>
                        <p><b>Version:</b>
                    <?php
                          if ($app[AppUpdater::INDEX_SUBTITLE]) {
                              echo $app[AppUpdater::INDEX_SUBTITLE] . " (" . $app[AppUpdater::INDEX_VERSION] . ")";
                          } else {
                              echo $app[AppUpdater::INDEX_VERSION];
                          }
                          echo "<br/>";
                          if ($app[AppUpdater::INDEX_APPSIZE]) {
                              echo "<b>Size:</b> " . round($app[AppUpdater::INDEX_APPSIZE] / 1024 / 1024, 1) . " MB<br/>";
                          }
                          echo "<b>Released:</b> " . date('m/d/Y H:i:s', $app[AppUpdater::INDEX_DATE]);
                    ?>
                        </p>
                        <a class="button" href="<?php echo $b . 'api/2/apps/' . $app[AppUpdater::INDEX_DIR] ?>?format=apk">Install Application</a>
                    <?php if ($app[AppUpdater::INDEX_NOTES]) : ?>
                        <p><br/><br/></p>
                        <p><b>What's New:</b><br/><?php echo $app[AppUpdater::INDEX_NOTES] ?></p>
                    <?php endif ?>
                    </div>
                <?php endforeach ?>
                </div>
            <?php } else if (DeviceDetector::$isAmazonDevice) { ?>
                <div class='amazon'>
                    <div style="margin-bottom:12px;">
                        <img src="images/kabam_logo.png" alt="" style="width:100px;height:55px;vertical-align:middle;"/>
                        <span style="margin-left:8px;font-size:14px;font-weight:bold;">SmokeBomb: App Installer</span>
                    </div>
                <?php
                    $amazonAppsAvailable = 0;

                    foreach ($apps->applications as $i => $app) :
                        if ($app[AppUpdater::INDEX_PLATFORM] == AppUpdater::APP_PLATFORM_AMAZON) {
                            $amazonAppsAvailable++;
                        }
                    endforeach;

                    if ($amazonAppsAvailable > 1) {
                ?>
                    <p class="bordertop"></p>
                    <div class="grid">
                <?php
                        $column= 0;
                        foreach ($apps->applications as $i => $app) :
                            if ($app[AppUpdater::INDEX_PLATFORM] != AppUpdater::APP_PLATFORM_AMAZON)
                                continue;

                            $column++;
                ?>
                        <div class="column span-4">
                            <a href="#<?php echo $app[AppUpdater::INDEX_APP] ?>">
                <?php if ($app[AppUpdater::INDEX_IMAGE]) { ?>
                                <img class="icon" src="<?php echo $b.$app[AppUpdater::INDEX_IMAGE] ?>">
                <?php } ?>
                                <h4><?php echo $app[AppUpdater::INDEX_APP] ?></h4>
                            </a>
                        </div>

                <?php
                            if ($column == 2) {
                                echo "<div style='clear:both;'></div>";
                                $column = 0;
                            }
                        endforeach;
                ?>
                    </div>
                <?php
                    }
                ?>
                    <div style='clear:both;'><br/></div>

                <?php if ($amazonAppsAvailable > 1) { ?>
                    <p><br/></p>
                <?php } ?>
                <?php
                    foreach ($apps->applications as $i => $app) :
                        if ($app[AppUpdater::INDEX_PLATFORM] != AppUpdater::APP_PLATFORM_AMAZON)
                            continue;
                ?>

                    <div class="version">
                        <p class="borderbottom"></p>
                        <a name="<?php echo $app[AppUpdater::INDEX_APP] ?>"><br/></a>
                    <?php if ($app[AppUpdater::INDEX_IMAGE]) { ?>
                        <img class="icon" src="<?php echo $b.$app[AppUpdater::INDEX_IMAGE] ?>">
                    <?php } ?>
                        <h2><?php echo $app[AppUpdater::INDEX_APP] ?></h2>
                        <p><b>Version:</b>
                    <?php
                          if ($app[AppUpdater::INDEX_SUBTITLE]) {
                              echo $app[AppUpdater::INDEX_SUBTITLE] . " (" . $app[AppUpdater::INDEX_VERSION] . ")";
                          } else {
                              echo $app[AppUpdater::INDEX_VERSION];
                          }
                          echo "<br/>";
                          if ($app[AppUpdater::INDEX_APPSIZE]) {
                              echo "<b>Size:</b> " . round($app[AppUpdater::INDEX_APPSIZE] / 1024 / 1024, 1) . " MB<br/>";
                          }
                          echo "<b>Released:</b> " . date('m/d/Y H:i:s', $app[AppUpdater::INDEX_DATE]);
                    ?>
                        </p>
                        <a class="button" href="<?php echo $b . 'api/2/apps/' . $app[AppUpdater::INDEX_DIR] ?>?format=apk">Install Application</a>
                    <?php if ($app[AppUpdater::INDEX_NOTES]) : ?>
                        <p><br/><br/></p>
                        <p><b>What's New:</b><br/><?php echo $app[AppUpdater::INDEX_NOTES] ?></p>
                    <?php endif ?>
                    </div>
                <?php endforeach ?>
                </div>
            <?php } else if (DeviceDetector::$isOldIOSDevice) { ?>
                <div class='old-ios'>

                    <h3>Direct Installation Not Supported</h3>

                    <p>You are running a version of iOS that does not support direct installation. Please visit this page on your Mac or PC to download an app.</p>
                    <p>If you are able to upgrade your device to iOS 4.0 or later, simply visit this page with your iPad, iPhone, or iPod touch and you can install an app directly on your device.</p>

                </div>
            <?php } else if (DeviceDetector::$isNewIOSDevice) { ?>
                <div class='new-ios'>

                    <div style="margin-bottom:12px;">
                    	<img src="../images/topimage.png" style="width:100%;height:15%;vertical-align:middle;"/>
		    </div>
		    <div align="center">
                    	<span style="font-family:Arial;font-size:20px;font-weight:bold;color:#7a8f9f">COK App Installer</span>
                    </div>

                    <p class="bordertop"></p>
                <?php
                    $iOSAppsAvailable = 0;
                    foreach ($apps->applications as $i => $app) : 
                        if ($app[AppUpdater::INDEX_PLATFORM] == AppUpdater::APP_PLATFORM_IOS) {
                            $iOSAppsAvailable++;
                        }
                    endforeach;

                    if ($iOSAppsAvailable > 0) { 
                ?>
                    <p class="bordertop"></p>
		    <div align="left" style="width:100%;height:30px;background-image:url(../images/titleitem.png);background-size:100% 100px;background-repeat:no-repeat;color:#89bae0">
                        <p style="font-family:Arial;margin-left:10px;font-size:18px;font-weight:bold;font-family:Arial">Latest version</p>
                    </div>
		<?php
                        $column= 0;
                        //foreach ($apps->applications as $i => $app) :
                        foreach ($ipaPayment as $i => $app) :
                            if ($app[AppUpdater::INDEX_PLATFORM] != AppUpdater::APP_PLATFORM_IOS)
                                continue;
                ?>
                        <div class="column span-4" align="center">
                            <a href="#<?php echo $app[AppUpdater::INDEX_APP] ?>" style="color:#e4edf4">
                                <h4 style="font-family:Arial"><?php echo $app[AppUpdater::INDEX_APP] ?></h4>
                            </a>
                        </div>

                <?php
                            break;
                        endforeach;
                ?>
                    <div> 
                        <!--span style="margin-left:8px;font-size:18px;font-weight:bold;">Dev versions</span-->
                        <div valign="middle" style="width:100%;height:30px;background-image:url(../images/titleitem.png);background-size:100% 100px;background-repeat:no-repeat;color:#89bae0">
			    <span style="font-family:Arial;margin-left:10px;font-size:18px;font-weight:bold;font-family:Arial">Dev versions</span>    
                            <a href='#' class='main' onclick='document.getElementById("devversions").style.display=="none"?document.getElementById("devversions").style.display="block":document.getElementById("devversions").style.display="none";'><img src="../images/buttondown.png" height=30px align="right"/></a>
                        </div>   
			<div style='display:all;' id="devversions">
                <?php
                        $column= 0;
                        //foreach ($apps->applications as $i => $app) :
                        foreach ($ipaPayment as $i => $app) :
                            if ($app[AppUpdater::INDEX_PLATFORM] != AppUpdater::APP_PLATFORM_IOS)
                                continue;

                            $column++;
                ?>
                        <div class="column span-4" align="center">
                            <a href="#<?php echo $app[AppUpdater::INDEX_APP] ?>">
                                <h4 style="color:#e4edf4;font-family:Arial"><?php echo $app[AppUpdater::INDEX_APP] ?></h4>
                            </a>
                        </div>
                <?php
                            if ($column == 2) {
                                echo "<div style='clear:both;'></div>";
                                $column = 0;
                            }
                        endforeach;
                ?>
                       </div>
                        </span>
                    </div>
                    <div>
                        <!--span style="margin-left:8px;font-size:18px;font-weight:bold;">Dev versions</span-->
                        <div valign="middle" style="width:100%;height:30px;background-image:url(../images/titleitem.png);background-size:100% 100px;background-repeat:no-repeat;color:#89bae0">    
                             <span style="font-family:Arial;margin-left:10px;font-size:18px;font-weight:bold;font-family:Arial">Ent versions</span>                   
                            <a href='#' class='main' onclick='document.getElementById("entversions").style.display=="none"?document.getElementById("entversions").style.display="block":document.getElementById("entversions").style.display="none";'><img src="../images/buttondown.png" height=30px align="right"/></a>
			</div>
		        <div style='display:all;' id="entversions">
                <?php
                        $column= 0;
                        //foreach ($apps->applications as $i => $app) :
                        foreach ($ipaAdHoc as $i => $app) :
                            if ($app[AppUpdater::INDEX_PLATFORM] != AppUpdater::APP_PLATFORM_IOS)
                                continue;

                            $column++;
                ?>
                        <div class="column span-4">
                            <a href="#<?php echo $app[AppUpdater::INDEX_APP] ?>">
                <?php if ($app[AppUpdater::INDEX_IMAGE]) { ?>
                                <img class="icon" src="<?php echo $b.$app[AppUpdater::INDEX_IMAGE] ?>">
                <?php } ?>
                                <h4 style="font-family:Arial;"color:#e4edf4"><?php echo $app[AppUpdater::INDEX_APP] ?></h4>
                            </a>
                        </div>

                <?php
                            if ($column == 2) {
                                echo "<div style='clear:both;'></div>";
                                $column = 0;
                            }
                        endforeach;
                ?>
                            </div>
                        </span>
                    </div>
                <?php
                    }
                ?>
                    <div style='clear:both;'><br/></div>
		<div><img src="../images/stylelines.png" height=100% width=100%/></div>
                <?php if ($iOSAppsAvailable > 1) { ?>
                    <p><br/></p>
                <?php } ?>
                <?php
                    foreach ($apps->applications as $i => $app) : 
                        if ($app[AppUpdater::INDEX_PLATFORM] != AppUpdater::APP_PLATFORM_IOS)
                            continue;
                ?>

                    <div class="version" style="background-image:url(../images/frame.png);background-size:100% 120%;background-repeat:no-repeat;">
                        <p class="borderbottom"></p>
                        <a name="<?php echo $app[AppUpdater::INDEX_APP] ?>"><br/></a>
                        <h3 style="font-family:Arial;color:#e4edf4"><?php echo $app[AppUpdater::INDEX_APP] ?></h3>
                        <p style="font-family:Arial;color:#7a8f9f"><b>Version:</b>
                    <?php
                          if (isset($app[AppUpdater::INDEX_SUBTITLE]) && $app[AppUpdater::INDEX_SUBTITLE]) {
                              echo $app[AppUpdater::INDEX_SUBTITLE] . " (" . $app[AppUpdater::INDEX_VERSION] . ")";
                          } else {
                              echo $app[AppUpdater::INDEX_VERSION];
                          }
                          echo "<br/>";
                          if ($app[AppUpdater::INDEX_APPSIZE]) {
                              echo "<b>Size:</b> " . round($app[AppUpdater::INDEX_APPSIZE] / 1024 / 1024, 1) . " MB<br/>";
                          }
                          echo "<b>Released:</b> " . date('m/d/Y H:i:s', $app[AppUpdater::INDEX_DATE]);
                    ?>
                        </p>
                        <?php if (isset($app[AppUpdater::INDEX_PROFILE]) && $app[AppUpdater::INDEX_PROFILE]) { ?>                    
                        <a class="button" href="<?php echo $b . 'api/2/apps/' . $app[AppUpdater::INDEX_DIR] ?>?format=mobileprovision">Install Profile</a>
                    <?php } ?>
                        <a class="button" align="center" style="color:#89bae0;font-size:20px" href="itms-services://?action=download-manifest&amp;url=<?php echo $b . $app[AppUpdater::INDEX_DIR] . "/IF.plist" ?>"><img align="left" src="../images/downloadicon.png" height="20px"/>&nbsp;&nbsp;Install Application</a>

						

                    <?php if ($app[AppUpdater::INDEX_NOTES]) : ?>
                        <p><br/><br/></p>
                        <p><b>What's New:</b><br/><?php echo $app[AppUpdater::INDEX_NOTES] ?></p>
                    <?php endif ?>
                    </div>
                <?php endforeach ?>
                </div>
            <?php } else if (DeviceDetector::$isiPad4Device) { ?>
                <div class='new-ios'>

                    <div style="margin-bottom:12px;">
                        <img src="../images/topimage.png" style="width:100%;height:15%;vertical-align:middle;"/>
                    </div>
                    <div align="center">
                        <span style="font-family:Arial;font-size:20px;font-weight:bold;color:#7a8f9f">COK App Installer</span>
                    </div>

                    <p class="bordertop"></p>
                    <?php
                        $iOSAppsAvailable = 0;
                        foreach ($apps->applications as $i => $app) :
                            if ($app[AppUpdater::INDEX_PLATFORM] == AppUpdater::APP_PLATFORM_IOS) {
                                    $iOSAppsAvailable++;
                            }
                        endforeach;
    
                        if ($iOSAppsAvailable > 0) {
                    ?>
                    <p class="bordertop"></p>
                    <div align="left" style="width:100%;height:30px;background-image:url(../images/titleitem.png);background-size:100% 100px;background-repeat:no-repeat;color:#89bae0">
                        <p style="font-family:Arial;margin-left:10px;font-size:18px;font-weight:bold;font-family:Arial">Latest version</p>
                    </div>
                    <?php
                        $column= 0;
                        //foreach ($apps->applications as $i => $app) :
                        foreach ($ipaPayment as $i => $app) :
                            if ($app[AppUpdater::INDEX_PLATFORM] != AppUpdater::APP_PLATFORM_IOS)
                            continue;
                    ?>
                    <div class="column span-4" align="center">
                        <a href="#<?php echo $app[AppUpdater::INDEX_APP] ?>" style="color:#e4edf4">
                        <h4 style="font-family:Arial"><?php echo $app[AppUpdater::INDEX_APP] ?></h4>
                        </a>
                    </div>

                    <?php
                            break;
                        endforeach;
                    ?>
                    <div>
                        <!--span style="margin-left:8px;font-size:18px;font-weight:bold;">Dev versions</span-->
                        <div valign="middle" style="width:100%;height:30px;background-image:url(../images/titleitem.png);background-size:100% 100px;background-repeat:no-repeat;color:#89bae0">
                            <span style="font-family:Arial;margin-left:10px;font-size:18px;font-weight:bold;font-family:Arial">Dev versions</span>
                            <a href='#' class='main' onclick='document.getElementById("devversions").style.display=="none"?document.getElementById("devversions").style.display="block":document.getElementById("devversions").style.display="none";'><img src="../images/buttondown.png" height=30px align="right"/></a>
                        </div>
                        <div style='display:all;' id="devversions">
                    <?php
                        $column= 0;
                        //foreach ($apps->applications as $i => $app) :
                        foreach ($ipaPayment as $i => $app) :
                            if ($app[AppUpdater::INDEX_PLATFORM] != AppUpdater::APP_PLATFORM_IOS)
                            continue;
    
                            $column++;
                    ?>
                    <div class="column span-4" align="center">
                        <a href="#<?php echo $app[AppUpdater::INDEX_APP] ?>">
                        <h4 style="color:#e4edf4;font-family:Arial"><?php echo $app[AppUpdater::INDEX_APP] ?></h4>
                        </a>
                    </div>
                    <?php
                            if ($column == 2) {
                                echo "<div style='clear:both;'></div>";
                                $column = 0;
                            }
                        endforeach;
                    ?>
                    </div>
                    </span>
                    </div>
                    <div>
                    <!--span style="margin-left:8px;font-size:18px;font-weight:bold;">Dev versions</span-->
                        <div valign="middle" style="width:100%;height:30px;background-image:url(../images/titleitem.png);background-size:100% 100px;background-repeat:no-repeat;color:#89bae0">
                        <span style="font-family:Arial;margin-left:10px;font-size:18px;font-weight:bold;font-family:Arial">Ent versions</span>
                    <a href='#' class='main' onclick='document.getElementById("entversions").style.display=="none"?document.getElementById("entversions").style.display="block":document.getElementById("entversions").style.display="none";'><img src="../images/buttondown.png" height=30px align="right"/></a>
                    </div>
                    <div style='display:all;' id="entversions">
                    <?php
                        $column= 0;
                        //foreach ($apps->applications as $i => $app) :
                        foreach ($ipaAdHoc as $i => $app) :
                            if ($app[AppUpdater::INDEX_PLATFORM] != AppUpdater::APP_PLATFORM_IOS)
                            continue;
    
                            $column++;
                    ?>
                    <div class="column span-4">
                        <a href="#<?php echo $app[AppUpdater::INDEX_APP] ?>">
                    <?php if ($app[AppUpdater::INDEX_IMAGE]) { ?>
                        <img class="icon" src="<?php echo $b.$app[AppUpdater::INDEX_IMAGE] ?>">
                    <?php } ?>
                        <h4 style="font-family:Arial;"color:#e4edf4"><?php echo $app[AppUpdater::INDEX_APP] ?></h4>
                            </a>
                    </div>

                    <?php
                            if ($column == 2) {
                                echo "<div style='clear:both;'></div>";
                                $column = 0;
                            }
                        endforeach;
                    ?>
                    </div>
                    </span>
                    </div>
                    <?php
                        }
                    ?>
                    <div style='clear:both;'><br/></div>
                    <div><img src="../images/stylelines.png" height=100% width=100%/></div>
                    <?php if ($iOSAppsAvailable > 1) { ?>
                        <p><br/></p>
                    <?php } ?>
                    <?php
                        foreach ($apps->applications as $i => $app) :
                            if ($app[AppUpdater::INDEX_PLATFORM] != AppUpdater::APP_PLATFORM_IOS)
                            continue;
                    ?>

                    <div class="version" style="background-image:url(../images/frame.png);background-size:100% 120%;background-repeat:no-repeat;">
                        <p class="borderbottom"></p>
                        <a name="<?php echo $app[AppUpdater::INDEX_APP] ?>"><br/></a>
                        <h3 style="font-family:Arial;color:#e4edf4"><?php echo $app[AppUpdater::INDEX_APP] ?></h3>
                        <p style="font-family:Arial;color:#7a8f9f"><b>Version:</b>
                    <?php
                        if (isset($app[AppUpdater::INDEX_SUBTITLE]) && $app[AppUpdater::INDEX_SUBTITLE]) {
                            echo $app[AppUpdater::INDEX_SUBTITLE] . " (" . $app[AppUpdater::INDEX_VERSION] . ")";
                        } else {
                            echo $app[AppUpdater::INDEX_VERSION];
                        }
                        echo "<br/>";
                        if ($app[AppUpdater::INDEX_APPSIZE]) {
                            echo "<b>Size:</b> " . round($app[AppUpdater::INDEX_APPSIZE] / 1024 / 1024, 1) . " MB<br/>";
                        }
                            echo "<b>Released:</b> " . date('m/d/Y H:i:s', $app[AppUpdater::INDEX_DATE]);
                        ?>
                        </p>
                        <?php if (isset($app[AppUpdater::INDEX_PROFILE]) && $app[AppUpdater::INDEX_PROFILE]) { ?>
                        <a class="button" href="<?php echo $b . 'api/2/apps/' . $app[AppUpdater::INDEX_DIR] ?>?format=mobileprovision">Install Profile</a>
                        <?php } ?>
                        <a class="button" align="center" style="color:#89bae0;font-size:20px" href="itms-services://?action=download-manifest&amp;url=<?php echo $b . $app[AppUpdater::INDEX_DIR] . "/IF.plist" ?>"><img align="left" src="../images/downloadicon.png" height="20px"/>&nbsp;&nbsp;Install Application</a>



                    <?php if ($app[AppUpdater::INDEX_NOTES]) : ?>
                    <p><br/><br/></p>
                    <p><b>What's New:</b><br/><?php echo $app[AppUpdater::INDEX_NOTES] ?></p>
                    <?php endif ?>
                    </div>
                    <?php endforeach ?>
            </div>

            <?php } else { ?>
                <div class='desktop'>

					<div style="text-align:center;">
	                    <div style="margin-bottom:12px;">
                        <img src="../images/topimage.png" style="width:100%;height:15%;vertical-align:middle;"/>
                    </div>
                    <div align="center">
                        <span style="font-family:Arial;font-size:20px;font-weight:bold;color:#7a8f9f">COK App Installer</span>
                    </div>
					</div>

                    <p style="font-family:Arial;color:#7a8f9f" class='hintdevice'>Visit this page directly from your your iPad, iPhone, iPod touch or Android device and you will be able to install an app directly on your device. (requires iOS 4.0 or later)</p>

                    <p style="font-family:Arial;color:#7a8f9f" class='hintdevice'><strong>iOS:</strong> If your device does not have iOS 4.0 or later, please download the provisioning profile and the app on your computer from this page and install it <a href="<?php echo $b ?>itunes-installation.html">manually</a> via iTunes.
                    </p>

                    <br/>
                    <p class="bordertop"><br/></p>

                <?php
                    $column= 0;
                    foreach ($apps->applications as $i => $app) :
                        $column++;
                ?>
                    <div class="column span-3">
                    <?php if ($app[AppUpdater::INDEX_IMAGE]) { ?>
                        <img class="icon" src="<?php echo $b.$app[AppUpdater::INDEX_IMAGE] ?>">
                    <?php } ?>
                    </div>
                    <div class="column span-8">
                        <h2><?php echo $app[AppUpdater::INDEX_APP] ?></h2>
                        <p style="font-family:Arial;color:#7a8f9f"><b>Version:</b>
                      <?php
                        if (isset($app[AppUpdater::INDEX_SUBTITLE]) && $app[AppUpdater::INDEX_SUBTITLE]) {
                            echo $app[AppUpdater::INDEX_SUBTITLE] . " (" . $app[AppUpdater::INDEX_VERSION] . ")";
                        } else {
                            echo $app[AppUpdater::INDEX_VERSION];
                        }
                        echo "<br/>";
                        if ($app[AppUpdater::INDEX_APPSIZE]) {
                            echo "<b style='font-family:Arial;color:#7a8f9f'>Size:</b> " . round($app[AppUpdater::INDEX_APPSIZE] / 1024 / 1024, 1) . " MB<br/>";
                        }
                        echo "<b style='font-family:Arial;color:#7a8f9f'>Released:</b> " . date('m/d/Y H:i:s', $app[AppUpdater::INDEX_DATE]);
                      ?>
                        </p>

                        <div class="desktopbuttons">
                    <?php if (isset($app[AppUpdater::INDEX_PROFILE]) && $app[AppUpdater::INDEX_PROFILE]) : ?>
                            <a style="font-family:Arial;color:#7a8f9f" class="button" href="<?php echo $b . 'api/2/apps/' . $app[AppUpdater::INDEX_DIR] ?>?format=mobileprovision">Download Profile </a>
                    <?php endif;
                    if ($app[AppUpdater::INDEX_PLATFORM] == AppUpdater::APP_PLATFORM_IOS) : ?>
                        <a style="font-family:Arial;color:#7a8f9f" class="button" href="<?php echo $b . $app['path'] ?>">Download Application</a>
                    <?php elseif ($app[AppUpdater::INDEX_PLATFORM] == AppUpdater::APP_PLATFORM_ANDROID) : ?>
                        <a style="font-family:Arial;color:#7a8f9f" class="button" href="<?php echo $b . $app['path'] ?>">Download Application</a>
                    <?php endif ?>
                        </div>

                    <?php if ($app[AppUpdater::INDEX_NOTES]) : ?>
                        <p><br/><br/></p>
                        <p style="font-family:Arial;color:#7a8f9f"><b>What's New:</b><br/><?php echo $app[AppUpdater::INDEX_NOTES] ?></p>
                    <?php endif ?>

                    </div>

                <?php 
                        if ($column == 2) {
                            echo "<div style='clear:both;'><br/><p  class='bordertop'><br/></p></div>";
                            $column = 0;
                        }
                    endforeach;
                ?>

                </div>
            <?php } ?>

        <script type="text/javascript" charset="utf-8">
            /mobile/i.test(navigator.userAgent) &&
            !window.location.hash &&
            setTimeout(function () { window.scrollTo(0, 1); }, 2000);
        </script>
    </body>
</html>

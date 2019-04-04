# Roarder
Roarder create php loader class.
# How to use
[![Download Roarder](https://a.fsdn.com/con/app/sf-download-button)](https://sourceforge.net/projects/roarder/files/latest/download)
	
	1. Download and install
	2. Go to target directory
	3. <shift+right click> Open command window here and type roarder

# Config
manual add file config roarder.json
    
    {"except": "(\\[.].*$|.*\\vendor\\.*)",
     "option": "only",
     "dependencies":[{
        "dir": "E:\\github\\classbank1",
        "option":"only",
        "except": "(\\[.].*$)"
        },{
        "dir": "E:\\github\\classbank2",
        "option":"all",
        "except": "(\\[.].*$|.*\\vendor\\.*)"
        }]
    }
### Config Parameters
    dir: path location
    option: 1. only
            2. all for recursive
    except: exclude string with regex and will combine with dir location
    
this is not dependency manager. just create autoloader from directory. 
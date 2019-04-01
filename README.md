# Roarder
Roarder create php loader class .
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

    dir: path location

    option: 1. only
            2. all for recursive

    except: exclude string with regex and will combine with dir location
    
this is not dependency manager like composer. 
# Roarder
Roarder create php loader class.
# How to use
	1. Download [sourceforge](https://sourceforge.net/projects/roarder/files/dev/Roarder.exe/download).
	2. Set PATH system variable to Roarder.exe <require restart>.
	3. Open CLI.
	4. Set Config roarder.json.
	4. Type Variable on CLI ex roarder.
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
    
this is not dependency manager like composer. 

### How do I set or change the PATH system variable?
	https://www.java.com/en/download/help/path.xml
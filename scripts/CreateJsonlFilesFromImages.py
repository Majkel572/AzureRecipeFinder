# Use this script in azure2 directory

import os
import json
from PIL import Image

os.listdir

categories = os.listdir('./images')



# Creating validation and training datasets
trainData = ''
validateData = ''
ratio = 5
ratioIterator = 0

for category in categories:
    pathDir = './images/'+category
    filenames = os.listdir(pathDir)
    
    for filename in filenames:

        path = pathDir + '/' + filename

        image = Image.open(path)

        # preparing json data
        jsondata = {}
        
        image_url = path # Find and fill

        image_details = {}
        image_details['format'] = image.format # Probably needed to be lowercase - to check
        image_details['width'] = str(image.width)
        image_details['height'] = str(image.height)

        label = category

        jsondata['image_url'] = image_url
        jsondata['image_details'] = image_details
        jsondata['label'] = label

        print(json.dumps(jsondata, indent=4))

        jsonDumped = json.dumps(jsondata)

        if ratioIterator % ratio == 0:
            validateData = validateData + jsonDumped + '\n'
        else:
            trainData = trainData + jsonDumped + '\n'

        ratioIterator = ratioIterator + 1

jsonDir = './data'

if not os.path.isdir(jsonDir):
    os.mkdir(jsonDir)

# Save training json
file = open(jsonDir + '/trainData.jsonl', 'w')
file.write(trainData)
file.close()

# Save Validate json
file = open(jsonDir + '/validateData.jsonl', 'w')
file.write(validateData)
file.close()


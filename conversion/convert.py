'''
The only module is json as this is a basic .py file
'''
import os
import json

path = os.getcwd()

try:
    with open(path + "\\conversion\\ASCII.json", "r", encoding="UTF-8") as f:
        # pylint: disable=invalid-name
        ASCII = json.load(f)
finally:
    f.close()


def locate_item(key: str, search_item: str) -> str:
    '''
    Locates the item in the json file
    '''
    for item in ASCII[key]:
        if search_item in item:
            return str(ASCII[key][search_item])
    return None


def give_value(line: str, conversion: str) -> str:
    '''
    Gives the value of the item in the json file
    '''
    text = ''

    for char in line:
        value = locate_item(conversion, char)
        if value is not None:
            text += value + " "

    return text.strip()

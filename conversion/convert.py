'''
The only module is json as this is a basic .py file
'''
import json

with open(".\\conversion\\ASCII.json", "r", encoding="UTF-8") as f:
    # pylint: disable=invalid-name
    ASCII = json.load(f)


def locate_item(key: str, search_item: str) -> str:
    '''
    Locates the item in the json file
    '''
    for item in ASCII[key]:
        if search_item in item:
            return str(ASCII[key][search_item])
    return None


def give_value(string_name: str, conversion: str) -> str:
    '''
    Gives the value of the item in the json file
    '''
    text = ''

    for char in string_name:
        value = locate_item(conversion, char)
        if value is not None:
            text += value + " "

    return text.strip()

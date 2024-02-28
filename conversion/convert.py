'''
    This is the backend to my conversion app.
    It has two functions:
    - locate_item(key -> str, search_item -> str) returns str index of item - search_item
    - give_value(line -> str, conversion -> str) returns values as str 
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
        This returns a single item finding.

        ```
        from convert import locate_item

        string = "A"
        conversion = "Hexadecimal"

        value_of_single_item = locate_item(conversion, string)

        print(value_of_single_item)
        ```

        Output:
        ```
        41
        ```
    '''
    for item in ASCII[key]:
        if search_item in item:
            return str(ASCII[key][search_item])
    return None


def give_value(line: str, conversion: str) -> str:
    '''
        This returns a full string item finding.

        ```
        from convert import give_value

        string = "string value"
        conversion = "Hexadecimal"

        value_of_full_string = give_value(string, conversion)

        print(value_of_full_string)
        ```

        Output:
        ```
        73 74 72 69 6E 67 20 76 61 6C 75 65
        ```
    '''
    text = ''

    for char in line:
        value = locate_item(conversion, char)
        if value is not None:
            text += value + " "

    return text.strip()

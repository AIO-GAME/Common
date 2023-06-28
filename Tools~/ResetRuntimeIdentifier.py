# -*- coding: UTF-8 -*-

import os

import os
import shutil

def delete_nonempty_folder(folder_path):
    try:
        shutil.rmtree(folder_path)
        print(f"成功删除非空文件夹：{folder_path}")
    except OSError as e:
        print(f"删除非空文件夹失败：{folder_path}，原因：{e}")

def find_obj_bin_folders(targetpath):
    folders = []
    for root, dirs, files in os.walk(targetpath):
        for dir_name in dirs:
            if dir_name == "obj" or dir_name == "bin":
                folders.append(os.path.join(root, dir_name))
    return folders

def main():
	print("\n======================================================================")
	print("- Author   :XiNan")
	print("- E-Mail   :1398581458@qq.com")
	print("- Welcome  :删除 OBJ BIN 文件夹")
	print("======================================================================\n")

	curDir = os.path.dirname(os.path.abspath(__file__))
	obj_bin_folders = find_obj_bin_folders(curDir)
	for folder in obj_bin_folders:
		delete_nonempty_folder(folder)

if __name__ == '__main__':
	main()
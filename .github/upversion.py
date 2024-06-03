# 升级bat标签版本
# 1. 读取当前版本号
# 2. 递增版本号
# 3. 写入新版本号 上传到远程仓库
# 4. 在当前分支的基础上创建新的分支 并切换到新的分支
# 5. 在新的分支上忽略指定文件和文件夹
# 6. 上传到远程仓库
# 7. 在新的分支上创建新的tag
# 8. 上传到远程仓库
# 9. 切换回当前分支
# 10. 删除新的分支

# 当前文件夹路径
import os
import json


def read_current_branch() -> str:
    branches = os.popen("git branch").read().split("\n")
    # 判断当前分支
    for branch in branches:
        if branch.startswith("*"):
            return branch.replace("* ", "")


def read_current_version() -> str:
    os.system("git fetch --tags")
    tags = os.popen("git tag").read().split("\n")
    # 所有标签去掉空字符串 -preview标签去掉preview 然后按照version排序
    tags = sorted([tag.replace("-preview", "") for tag in tags if tag], key=lambda x: tuple(map(int, x.split("."))))
    return tags[-1]


# 切换上一级目录
os.chdir(os.path.dirname(os.path.dirname(os.path.realpath(__file__))))

current_path = os.getcwd()
print("当前路径: " + current_path)

# 是否为preview版本
is_preview = True

# 忽略列表
ignore_list = [
    ".git",
    ".idea",
    "*.yaml",
    "*.yml"
]

# 读取当前分支
current_branch = read_current_branch()

# 读取当前版本号
version = read_current_version()
print("当前版本号: " + version)

# 递增版本号
version_list = version.split(".")
if is_preview:
    version_list[2] = str(int(version_list[2]) + 1) + "-preview"
else:
    version_list[2] = str(int(version_list[2]) + 1)
new_version = ".".join(version_list)

# 写入新版本号
with open("package.json", "r+") as f:
    package = json.load(f)
    package["version"] = new_version
    f.seek(0)
    json.dump(package, f, indent=2)
    f.close()
print("写入新版本号成功: " + new_version)

# 上传到远程仓库 捕获异常
try:
    os.system("git add package.json")
    os.system("git commit -m 'up version'")
    os.system("git push origin " + current_branch)
    print("上传到远程仓库({0})成功".format(current_branch))
except Exception as e:
    print("上传到远程仓库({0})失败".format(current_branch))
    print(e)

# 
# # 在当前分支的基础上创建新的分支 并切换到新的分支
# os.system("git checkout -b release/" + new_version)
# 
# # 在新的分支上忽略指定文件和文件夹
# with open(".gitignore", "a") as f:
#     for ignore in ignore_list:
#         f.write(ignore + "\n")
#     print("忽略文件和文件夹成功")
# 
# # 上传到远程仓库
# os.system("git add .")
# os.system("git commit -m 'up version'")
# os.system("git push origin release/" + new_version)
# 
# # 在新的分支上创建新的tag
# os.system("git tag -a " + new_version + " -m 'up version'")
# os.system("git push origin " + new_version)
# 
# # 切换回当前分支
# os.system("git checkout " + current_branch)
# 
# # 删除新的分支
# os.system("git branch -D release/" + new_version)
# print("删除新的分支成功")
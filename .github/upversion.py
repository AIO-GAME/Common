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
import shutil
import time
import json
import os
import subprocess
import stat


def get_local_tags() -> set[str]:
    result = subprocess.run(['git', 'tag'], stdout=subprocess.PIPE)
    tags = result.stdout.decode('utf-8').split()
    return set(tags)


def get_remote_tags(remote_name='origin') -> set[str]:
    result = subprocess.run(['git', 'ls-remote', '--tags', remote_name], stdout=subprocess.PIPE)
    remote_tags_output = result.stdout.decode('utf-8')
    remote_tags = set()
    for line in remote_tags_output.strip().split('\n'):
        if line:
            tag_ref = line.split()[1]
            if tag_ref.startswith('refs/tags/'):
                remote_tags.add(tag_ref[len('refs/tags/'):])
    return remote_tags


def delete_local_tag(tag) -> None:
    os.system(f'git tag -d {tag}')
    print(f"Deleted local tag {tag}")


def delete_remote_tag() -> None:
    os.system('git fetch --prune origin +refs/tags/*:refs/tags/*')
    local_tags = get_local_tags()
    remote_tags = get_remote_tags()
    tags_to_delete = local_tags - remote_tags

    for tag in tags_to_delete:
        delete_local_tag(tag)


# 处理只读文件删除问题的回调函数
def remove_readonly(func, path, _) -> None:
    os.chmod(path, stat.S_IWRITE)
    func(path)


# 删除文件夹
def delete_folder(folder_path) -> None:
    try:
        if os.path.exists(folder_path):
            shutil.rmtree(folder_path, onerror=remove_readonly)
            print(f"文件夹 '{folder_path}' 已成功删除。")
        else:
            print(f"文件夹 '{folder_path}' 不存在。")
    except Exception as exp:
        print(f"文件夹 '{folder_path}' 删除失败。")
        print(exp)


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
    ".chglog",
    "*.yaml",
    "*.yml",
    "Tools~",
    ".github/API_USAGE/",
    ".github/ISSUE_TEMPLATE/",
    ".github/PULL_REQUEST_TEMPLATE/",
    ".github/Template/",
    ".github/workflows/",
    ".github/*.py",
    ".github/*.sh",
    ".github/*.bat",
]
github = os.popen("git remote get-url origin").read().strip()
print("github 地址: " + github)

delete_remote_tag()
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
    current_version = package["version"]
    if current_version != new_version:
        package["version"] = new_version
        f.seek(0)
        json.dump(package, f, indent=2)
        print("写入新版本号成功: {0} -> {1}".format(current_version, new_version))
    f.close()

# 上传到远程仓库 捕获异常
if current_version == new_version:
    print("版本号没有变化")
else:
    try:
        os.system("git pull")
        os.system("git add package.json")
        os.system("git commit -m \"✨ up version {0} -> {1}\"".format(current_branch, new_version))
        os.system("git push origin " + current_branch)
        print("上传到远程仓库({0})成功".format(current_branch))
    except Exception as e:
        print("上传到远程仓库({0})失败".format(current_branch))
        print(e)

# 克隆指定分支 到目标文件夹路径
os.chdir(os.path.dirname(current_path))
new_branch_path = os.path.join(os.path.dirname(current_path), new_version)
print("新分支路径: " + new_branch_path + " 是否存在: " + str(os.path.exists(new_branch_path)))

if os.path.exists(new_branch_path) is False:
    cmd = "git clone {0} -b {1} --single-branch {2}".format(github, current_branch, new_branch_path)
    os.system(cmd)

# 切换环境变量路径 为指定分支路径
os.chdir(new_branch_path)
os.system("git reset --hard")
os.system("git pull")

# 在当前分支的基础上创建新的分支 并切换到新的分支
# 在远端创建分支
new_branch = "release/{0}_{1}".format(new_version, str(int(time.time())))
os.system("git checkout -b {0}".format(new_branch))
print("创建新的分支成功: {0}".format(new_branch))

# 在新的分支上忽略指定文件和文件夹 如果没有则创建 如果有则拼接
with open(os.path.join(new_branch_path, ".gitignore"), "a+") as f:
    for ignore in ignore_list:
        if ignore.startswith("*"):
            f.write(ignore + "\n")
        else:
            f.write("/" + ignore + "\n")
    print("忽略文件和文件夹成功")

# 删除指定文件和文件夹
for ignore in ignore_list:
    os.system("git rm -r --cached " + ignore)
    print("删除文件和文件夹成功: " + ignore)

os.system("git add .")
os.system("git commit -m \"✨ up version\"")
os.system("git push origin {0}".format(new_branch))  # 上传到远程仓库
# 使用 GPG 签名
os.system("git tag -a {0} -m \"✨ up version {1}\"".format(new_version, new_version))  # 在新的分支上创建新的tag
os.system("git push origin {0}".format(new_version))  # 上传到远程仓库

# 删除远端分支
os.system("git push origin --delete {0}".format(new_branch))

# 切换回当前分支
os.chdir(current_path)
delete_folder(new_branch_path)
print("升级标签版本成功")
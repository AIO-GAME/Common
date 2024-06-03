@rem 升级bat标签版本
@rem 1. 读取当前版本号
@rem 2. 递增版本号
@rem 3. 写入新版本号 上传到远程仓库
@rem 4. 在当前分支的基础上创建新的分支 并切换到新的分支
@rem 5. 在新的分支上忽略指定文件和文件夹
@rem 6. 上传到远程仓库
@rem 7. 在新的分支上创建新的tag
@rem 8. 上传到远程仓库
@rem 9. 切换回当前分支
@rem 10. 删除新的分支

@rem 当前文件夹路径
@set current_path=%cd%

@rem 是否为preview版本
@set preview=false

@rem 忽略列表
@set ignoreList=(.vscode node_modules dist)

@rem 记录当前分支
@for /f "tokens=2" %%i in ('git branch') do set branch=%%i
@echo 当前分支：%branch%

@rem 读取当前版本号 文件./package.json "version": "1.0.0",
@for /f "tokens=2 delims=: " %%i in ('findstr /c:"\"version\":" ./package.json') do set version=%%i
@echo 当前版本号：%version%

@rem 递增版本号
@for /f "tokens=1-3 delims=." %%i in ("%version%") do (
    set major=%%i
    set minor=%%j
    set patch=%%k
)
@set /a patch+=1

@rem 写入新版本号 判断是否为preview版本
@set version=%major%.%minor%.%patch%
@if %preview%==true set version=%version%-preview
@echo 新版本号：%version%
@echo { "version": "%version%" } > package.json

@rem 上传到远程仓库
@git add package.json
@git commit -m "up version %version%"
@git push

@rem 在当前分支的基础上创建新的分支 并切换到新的分支
@git checkout -b %version%

@rem 在新的分支上忽略指定文件和文件夹
@rem @for %%i in %ignoreList% do echo %%i/ >> .gitignore
@rem @git add .gitignore
@rem @git commit -m "ignore files"

@rem 上传到远程仓库
@git push --set-upstream origin %version%

@rem 在新的分支上创建新的tag
@git tag %version%
@git push origin %version%

@rem 切换回当前分支
@git checkout %branch%

@rem 删除新的分支


@pause
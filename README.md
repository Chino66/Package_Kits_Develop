# PackageKits

项目地址：[https://github.com/Chino66/Package\_Kits\_Develop](https://github.com/Chino66/Package_Kits_Develop)

提供对Unity的UPM包操作的基本功能封装。

## API

添加一个包

> PackageUtils.AddPackageAsync(packageId, callback);

更新一个包

> PackageUtils.UpdatePackageAsync(packageId, callback);

删除一个包

> PackageUtils.RemovePackageAsync(packageName, callback);

获取所有包列表

> PackageUtils.ListPackageAsync(callback);

查询包是否存在

> PackageUtils.HasPackageAsync(packageName);
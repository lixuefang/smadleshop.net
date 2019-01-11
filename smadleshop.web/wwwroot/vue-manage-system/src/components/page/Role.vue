<template>
    <div class="table">
        <div class="crumbs">
            <el-breadcrumb separator="/">
                <el-breadcrumb-item><i class="el-icon-lx-cascades"></i> 权限管理</el-breadcrumb-item>
            </el-breadcrumb>
        </div>
        <div class="container">
            <div class="handle-box">
                <el-button type="primary" icon="add" class="handle-del mr10" @click="handleAdd">新增</el-button>
            </div>
            <el-table :data="tableData" border class="table" ref="multipleTable">
                <el-table-column prop="roleName" label="权限名">
                </el-table-column>
                <el-table-column label="操作" width="180" align="center">
                    <template slot-scope="scope">
                        <el-button type="text" icon="el-icon-edit" @click="handleEdit(scope.$index, scope.row)">编辑</el-button>
                        <el-button type="text" icon="el-icon-delete" class="red" @click="handleDelete(scope.$index, scope.row)">删除</el-button>
                    </template>
                </el-table-column>
            </el-table>
        </div>

        <!-- 新增弹出框 -->
        <el-dialog title="新增" :visible.sync="addVisible" width="40%">
            <el-form ref="form" :model="form" label-width="80px">
                <el-form-item label="权限名">
                    <el-input v-model="form.roleName"></el-input>
                </el-form-item>
            </el-form>
            <el-tree ref="tree" :data="form.menuList"
                     show-checkbox
                     node-key="index"
                     default-expand-all
                     :expand-on-click-node="false">
                <span class="custom-tree-node" slot-scope="{ node, data }">
                    <span>{{ node.label }}</span>
                    <span class="btnlist">
                        <el-checkbox :label="button.value" v-model="button.checked" v-for="button in data.buttons"></el-checkbox>
                    </span>
                </span>
            </el-tree>
            <span slot="footer" class="dialog-footer">
                <el-button @click="addVisible = false">取 消</el-button>
                <el-button type="primary" @click="saveAdd">确 定</el-button>
            </span>
        </el-dialog>

        <!-- 编辑弹出框 -->
        <el-dialog title="编辑" :visible.sync="editVisible" width="60%">
            <el-form ref="form" :model="form" label-width="80px">
                <el-form-item label="权限名">
                    <el-input v-model="form.roleName"></el-input>
                </el-form-item>
            </el-form>
            <el-tree ref="tree" :data="form.menuList"
                     show-checkbox
                     node-key="index"
                     default-expand-all
                     :expand-on-click-node="false">
                <span class="custom-tree-node" slot-scope="{ node, data }">
                    <span>{{ node.label }}</span>
                    <span class="btnlist">
                        <el-checkbox :label="button.value" v-model="button.checked" v-for="button in data.buttons"></el-checkbox>
                    </span>
                </span>
            </el-tree>
            <span slot="footer" class="dialog-footer">
                <el-button @click="editVisible = false">取 消</el-button>
                <el-button type="primary" @click="saveEdit">确 定</el-button>
            </span>
        </el-dialog>

        <!-- 删除提示框 -->
        <el-dialog title="提示" :visible.sync="delVisible" width="300px" center>
            <div class="del-dialog-cnt">删除不可恢复，是否确定删除？</div>
            <span slot="footer" class="dialog-footer">
                <el-button @click="delVisible = false">取 消</el-button>
                <el-button type="primary" @click="deleteRow">确 定</el-button>
            </span>
        </el-dialog>
    </div>
</template>

<script>
    import sidebar from "../common/Sidebar";
    export default {
        name: 'role',
        data() {
            return {
                url: '/sysuser/list',
                emptyUrl:'./emptyRole.json',
                tableData: [],
                addVisible: false,
                editVisible: false,
                delVisible: false,
                //menuData: sidebar.data().items,
                form: {},
                idx: -1
            }
        },
        created() {
            this.getData();
        },
        methods: {
            getData() {
                // 开发环境使用 json 文件
                if (process.env.NODE_ENV === 'development') {
                    this.url = './role.json';
                };
                this.$axios.get(this.url).then((res) => {
                    this.tableData = res.data.list;
                })
            },
            handleEdit(index, row) {
                this.idx = index;
                this.form = this.tableData[index];
                this.editVisible = true;
                this.$nextTick(() => {
                    this.$refs.tree.setCheckedKeys(this.form.checkedMenu);
                });
            },
            handleDelete(index, row) {
                this.idx = index;
                this.delVisible = true;
            },
            handleAdd() {
                this.addVisible = true;
                this.$axios.get(this.emptyUrl).then((res) => {
                    this.form = res.data;
                })
            },
            // 保存新增
            saveAdd() {
                this.$axios.post("/SysRole/Add", this.form).then((res) => {
                    if (res.data.code == 1) {
                        this.addVisible = false;
                        this.$message.success('新增成功');
                    } else {
                        this.$message.error(res.data.msg);
                    }
                })
            },
            // 保存编辑
            saveEdit() {
                this.checkedMenu= getCheckedKeys();
                this.$axios.post("/SysRole/Edit", this.form).then((res) => {
                    if (res.data.code == 1) {
                        this.$set(this.tableData, this.idx, this.form);
                        this.editVisible = false;
                        this.$message.success('修改成功');
                    } else {
                        this.$message.error(res.data.msg);
                    }
                })
            },
            // 确定删除
            deleteRow() {
                this.$axios.post("/SysRole/Del", this.form).then((res) => {
                    if (res.data.code == 1) {
                        this.tableData.splice(this.idx, 1);
                        this.delVisible = false;
                        this.$message.success('删除成功');
                    } else {
                        this.$message.error(res.data.msg);
                    }
                })
            }
        }
    }

</script>

<style scoped>
    .handle-box {
        margin-bottom: 20px;
    }

    .handle-select {
        width: 120px;
    }

    .handle-input {
        width: 300px;
        display: inline-block;
    }

    .del-dialog-cnt {
        font-size: 16px;
        text-align: center
    }

    .table {
        width: 100%;
        font-size: 14px;
    }

    .red {
        color: #ff0000;
    }

    .mr10 {
        margin-right: 10px;
    }

    .btnlist {
        margin-left: 100px;
    }
</style>

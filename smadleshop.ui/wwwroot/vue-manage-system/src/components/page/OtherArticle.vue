<template>
    <div class="table">
        <div class="crumbs">
            <el-breadcrumb separator="/">
                <el-breadcrumb-item><i class="el-icon-lx-cascades"></i>其它管理</el-breadcrumb-item>
            </el-breadcrumb>
        </div>
        <div class="container">
            <div class="handle-box">
                <el-button type="primary" icon="add" class="handle-del mr10" @click="handleAdd">新增</el-button>
                <el-input v-model="select_word" placeholder="请输入标题" class="handle-input mr10"></el-input>
                <el-button type="primary" icon="search" @click="search">搜索</el-button>
            </div>
            <el-table :data="tableData" border class="table">
                <el-table-column prop="title" label="标题">
                </el-table-column>
                <el-table-column prop="createTime" label="添加日期">
                </el-table-column>
                <el-table-column label="操作" width="180" align="center">
                    <template slot-scope="scope">
                        <el-button type="text" icon="el-icon-edit" @click="handleEdit(scope.$index, scope.row)">编辑</el-button>
                    </template>
                </el-table-column>
            </el-table>
            <div class="pagination">
                <el-pagination background @current-change="handleCurrentChange" layout="prev, pager, next" :page-size="20" :total="recondCount">
                </el-pagination>
            </div>
        </div>

        <!-- 编辑弹出框 -->
        <el-dialog title="编辑" :visible.sync="editVisible" width="30%">
            <el-form ref="form" :model="form" label-width="80px">
                <el-form-item label="标题">
                    <el-input v-model="form.title"></el-input>
                </el-form-item>
                <div class="plugins-tips">
                    内容详情
                </div>
                <quill-editor ref="myTextEditor" v-model="form.summary" :options="editorOption"></quill-editor>
            </el-form>
            <span slot="footer" class="dialog-footer">
                <el-button @click="editVisible = false">取 消</el-button>
                <el-button type="primary" @click="saveEdit">确 定</el-button>
            </span>
        </el-dialog>
    </div>
</template>

<script>
    import 'quill/dist/quill.core.css';
    import 'quill/dist/quill.snow.css';
    import 'quill/dist/quill.bubble.css';
    import { quillEditor } from 'vue-quill-editor';
    export default {
        name: 'basetable',
        data() {
            return {
                url: '/SiteOther/list',
                tableData: [],
                roleList:[],
                cur_page: 1,
                recondCount:0,
                select_word: '',
                addVisible: false,
                editVisible: false,
                delVisible: false,
                form: {},
                idx: -1
            }
        },
        created() {
            this.getData();
            this.getRoleList();
        },
        components: {
            quillEditor
        },
        methods: {
            // 分页导航
            handleCurrentChange(val) {
                this.cur_page = val;
                this.getData();
            },
            // 获取 easy-mock 的模拟数据
            getData() {
                // 开发环境使用 json 文件
                if (process.env.NODE_ENV === 'development') {
                    this.url = './vuetable.json';
                };
                this.$axios.get(this.url, {
                    params: {
                        page: this.cur_page,
                        keyword: this.select_word
                    }
                }).then((res) => {
                    this.tableData = res.data.list;
                    this.recondCount = res.data.recondCount;
                })
            },
            search() {
                this.cur_page = 1;
                this.getData();
            },
            handleEdit(index, row) {
                this.idx = index;
                this.form = this.tableData[index];
                this.editVisible = true;
            },
            // 保存编辑
            saveEdit() {
                this.$axios.post("/SiteOther/Edit", this.form).then((res) => {
                    if (res.data.code == 1) {
                        this.$set(this.tableData, this.idx, this.form);
                        this.editVisible = false;
                        this.$message.success('修改成功');
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
</style>

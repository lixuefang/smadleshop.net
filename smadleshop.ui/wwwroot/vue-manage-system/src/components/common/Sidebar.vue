<template>
    <div class="sidebar">
        <el-menu class="sidebar-el-menu" :default-active="onRoutes" :collapse="collapse" background-color="#324157"
            text-color="#bfcbd9" active-text-color="#20a0ff" unique-opened router>
            <template v-for="item in menuList">
                <template v-if="item.children">
                    <el-submenu :index="item.index" :key="item.index">
                        <template slot="title">
                            <i :class="item.icon"></i><span slot="title">{{ item.label }}</span>
                        </template>
                        <template v-for="subItem in item.children">
                            <el-submenu v-if="subItem.children" :index="subItem.index" :key="subItem.index">
                                <template slot="title">
                                    {{ subItem.label }}
                                </template>
                                <el-menu-item v-for="(threeItem,i) in subItem.children" :key="i" :index="threeItem.index">
                                    {{ threeItem.label }}
                                </el-menu-item>
                            </el-submenu>
                            <el-menu-item v-else :index="subItem.index" :key="subItem.index">
                                {{ subItem.label }}
                            </el-menu-item>
                        </template>
                    </el-submenu>
                </template>
                <template v-else>
                    <el-menu-item :index="item.index" :key="item.index">
                        <i :class="item.icon"></i><span slot="title">{{ item.label }}</span>
                    </el-menu-item>
                </template>
            </template>
        </el-menu>
    </div>
</template>

<script>
    import bus from '../common/bus';
    export default {
        data() {
            return {
                url: '/home/getMenuRight',
                collapse: false,
                menuList: [
                    {
                        icon: 'el-icon-lx-home',
                        index: 'dashboard',
                        label: '系统首页',
                        level: 1,
                        show: true,
                        buttons:[]
                    },
                    {
                        icon: 'el-icon-lx-calendar',
                        index: '3',
                        label: '系统管理',
                        level: 1,
                        show: true,
                        buttons: [],
                        children: [
                            {
                                index: 'role',
                                label: '角色管理',
                                level: 2,
                                show: true,
                                buttons: [{ key: 'role_add', value: '新增', enabled: true }, { key: 'role_edit', value: '编辑', enabled: true }, { key: 'role_del', value: '删除', enabled: true }]
                            },
                            {
                                index: 'user',
                                label: '用户管理',
                                level: 2,
                                show: true,
                                buttons: [{ key: 'user_add', value: '新增', enabled: true }, { key: 'user_edit', value: '编辑', enabled: true }, { key: 'user_del', value: '删除', enabled: true }]
                            },
                            {
                                index: 'set',
                                label: '系统设置',
                                level: 2,
                                show: true
                            }
                        ]
                    },
                    {
                        icon: 'el-icon-lx-calendar',
                        index: '4',
                        label: '网站管理',
                        level: 1,
                        show: true,
                        buttons: [],
                        children: [
                            {
                                index: 'siteUser',
                                label: '用户管理',
                                level: 2,
                                show: true,
                                buttons: [{ key: 'siteUser_add', value: '新增', enabled: true }, { key: 'siteUser_edit', value: '编辑', enabled: true }, { key: 'siteUser_del', value: '删除', enabled: true  }]
                            },
                            {
                                index: 'contactUs',
                                label: '留言管理',
                                level: 2,
                                show: true
                            },
                            {
                                index: 'question',
                                label: '问题管理',
                                level: 2,
                                show: true
                            },
                            {
                                index: 'resource',
                                label: '资源',
                                level: 2,
                                show: true
                            }
                        ]
                    },
                    {
                        icon: 'el-icon-lx-cascades',
                        index: 'table',
                        label: '基础表格'
                    },
                    {
                        icon: 'el-icon-lx-copy',
                        index: 'tabs',
                        label: 'tab选项卡'
                    },
                    {
                        icon: 'el-icon-lx-calendar',
                        index: '5',
                        label: '表单相关',
                        children: [
                            {
                                index: 'form',
                                label: '基本表单'
                            },
                            {
                                index: '3-2',
                                label: '三级菜单',
                                children: [
                                    {
                                        index: 'editor',
                                        label: '富文本编辑器'
                                    },
                                    {
                                        index: 'markdown',
                                        label: 'markdown编辑器'
                                    },
                                ]
                            },
                            {
                                index: 'upload',
                                label: '文件上传'
                            }
                        ]
                    },
                    {
                        icon: 'el-icon-lx-emoji',
                        index: 'icon',
                        label: '自定义图标'
                    },
                    {
                        icon: 'el-icon-lx-favor',
                        index: 'charts',
                        label: 'schart图表'
                    },
                    {
                        icon: 'el-icon-rank',
                        index: '6',
                        label: '拖拽组件',
                        children: [
                            {
                                index: 'drag',
                                title: '拖拽列表',
                            },
                            {
                                index: 'dialog',
                                label: '拖拽弹框',
                            }
                        ]
                    },
                    {
                        icon: 'el-icon-lx-warn',
                        index: '7',
                        label: '错误处理',
                        children: [
                            {
                                index: 'permission',
                                label: '权限测试'
                            },
                            {
                                index: '404',
                                label: '404页面'
                            }
                        ]
                    }
                ]
            }
        },
        computed:{
            onRoutes(){
                return this.$route.path.replace('/','');
            }
        },
        created() {
            if (process.env.NODE_ENV === 'development') {
                this.url = './emptyRole.json';
            };
            this.$axios.get(this.url, { params: sessionStorage.getItem("id") }).then(res => {
                this.menuList = res.data.menuList;
            });
            // 通过 Event Bus 进行组件间通信，来折叠侧边栏
            bus.$on('collapse', msg => {
                this.collapse = msg;
            })
        }
    }
</script>

<style scoped>
    .sidebar{
        display: block;
        position: absolute;
        left: 0;
        top: 70px;
        bottom:0;
        overflow-y: scroll;
    }
    .sidebar::-webkit-scrollbar{
        width: 0;
    }
    .sidebar-el-menu:not(.el-menu--collapse){
        width: 250px;
    }
    .sidebar > ul {
        height:100%;
    }
</style>

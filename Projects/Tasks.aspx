<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Tasks.aspx.cs" Inherits="lmaoooo" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">

    <title>Collapsible sidebar using Bootstrap 3</title>

    <!-- Bootstrap CSS CDN -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <!-- Our Custom CSS -->
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="../css/DashUI.css" />
    <link rel="stylesheet" type="text/css" href="../css/task.css" />
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
</head>
<body>


    <div class="wrapper">
           <!-- Sidebar Holder -->
            <nav id="sidebar">
                <div class="sidebar-header">
                    <h3>DASH</h3>
                    <strong>DASH</strong>
                </div>

                <ul class="list-unstyled components">
                    <li class="active">


                        <a href="../Dashboard.aspx">
                            <i class="glyphicon glyphicon-briefcase"></i>
                            Dashboard
                        </a>
                        <a href="#homeSubmenu" data-toggle="collapse" aria-expanded="false">
                            <i class="glyphicon glyphicon-home"></i>
                            Departments
                        </a>

                        <%-- Need repeater here --%>
                        <ul class="collapse list-unstyled" id="homeSubmenu">
                            <asp:Repeater ID="Repeater2" runat="server">
                            <ItemTemplate>
                                <li><a href="../Projects/Project.aspx?Name=+<%# Eval("DepartmentID")%>"><%# Eval("DepartmentName")%></a></li>
                            </ItemTemplate>
                        </asp:Repeater>
                        </ul>
                    </li>
                    <li>

                        <a href="#pageSubmenu" data-toggle="collapse" aria-expanded="false">
                            <i class="glyphicon glyphicon-duplicate"></i>
                            Private Boards
                        </a>
                        <ul class="collapse list-unstyled" id="pageSubmenu">
                            <li><a href="#">Page 1</a></li>
                            <li><a href="#">Page 2</a></li>
                            <li><a href="#">Page 3</a></li>
                        </ul>
                    </li>
                    <li>
                        <a href="../Timesheet.aspx">
                            <i class="glyphicon glyphicon-link"></i>
                            TimeSheet
                        </a>
                    </li>

                </ul>

                <%--<ul class="list-unstyled CTAs">
                    <li><a href="https://bootstrapious.com/tutorial/files/sidebar.zip" class="download">Download source</a></li>
                    <li><a href="https://bootstrapious.com/p/bootstrap-sidebar" class="article">Back to article</a></li>
                </ul>--%>
            </nav>

            <!-- Page Content Holder -->
            <div id="content">

                <nav class="navbar navbar-default">
                    <div class="container-fluid">

                        <div class="navbar-header">
                            <button type="button" id="sidebarCollapse" class="btn btn-info navbar-btn">
                                <i class="glyphicon glyphicon-align-left"></i>
                                <span>Toggle Sidebar</span>
                            </button>
                        </div>

                        <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
                            <ul class="nav navbar-nav navbar-right">
                                <li><a href="#">Page</a></li>
                                <li><a href="#">Page</a></li>
                                <li><a href="#">Page</a></li>
                                <li><a href="#">Page</a></li>
                            </ul>
                        </div>
                    </div>
                </nav>

                <h2>Collapsible Sidebar Using Bootstrap 3</h2>
                <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</p>
                <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</p>





                <!-- Simple MDL Progress Bar -->
                <div id="p1" class="mdl-progress mdl-js-progress"></div>
                <script>
                    document.querySelector('#p1').addEventListener('mdl-componentupgraded', function () {
                        this.MaterialProgress.setProgress(44);
                    });
                </script>

                <div class="dd">



                    <asp:Label runat="server" ID="Label1"></asp:Label><br /><br />

                    <form id="frm" runat="server">
                        <p id="saveNotif"></p>

                        <%--MAIN REPEATER CODE VERY IMPORTANT--%>
                        <asp:Repeater ID="ParentRepeater" runat="server" OnItemDataBound="ItemBound" OnItemCommand="RepeaterDetailsRow_ItemCommand">
                            <ItemTemplate>
                                <!-- Repeated data -->
                                <ol class="kanban To-do">
                                    <h2><%# Eval("PhaseName")%></h2>
                                    <asp:Label ID="lblName" runat="server" Visible="false" Text='<%#Eval("PhaseID") %>'></asp:Label>
                                    <h3 style="display:none"><%#Eval("PhaseID") %></h3>
                                    <asp:Repeater ID="ChildRepeater" runat="server" OnItemDataBound="ChildRepeater_ItemDataBound">
                                        <ItemTemplate>
                                            <!-- Nested repeated data -->
                                            <li class="dd-item">
                                                <asp:Label ID="Label2" runat="server" Visible="false" Text='<%#Eval("Position") %>'></asp:Label>
                                                <h3 class="title dd-handle"><b><%# Eval("TaskName")%> </b><i class=" material-icons ">filter_none</i></h3>
                                                <div class="text" contenteditable="true">

                                                    <b><%# Eval("EmployeeName") %></b><br />
                                                </div>
                                                <i class="material-icons" id="label blue">label</i><div class="actions">
                                                    <i class="material-icons" id="color">palette</i><i class="material-icons">edit</i><i class="material-icons">insert_link</i><i class="material-icons">attach_file</i>
                                                </div>

                                            </li>

                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <div class="actions">
                                        <asp:Button runat="server" class="addbutt" CommandName="addnew"><%--<i class="material-icons">control_point</i>--%></asp:Button>
                                    </div>
                                </ol>

                            </ItemTemplate>
                        </asp:Repeater>

                    </form>
                    <menu class="kanban">
                    <button><i class="material-icons">playlist_add</i> Add new Column</button>
                    <button id="update"><i class="material-icons"></i>Update</button>
                    </menu>


                </div>
                <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
     <div id="chart_div"></div>
            </div>
        </div>






    <!-- jQuery CDN -->
    <script src="https://code.jquery.com/jquery-1.12.0.min.js"></script>
    <!-- Bootstrap Js CDN -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <%--<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>--%>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#sidebarCollapse').on('click', function () {
                $('#sidebar').toggleClass('active');
            });
        });
    </script>
    <script type="text/javascript">
        /*!
* Nestable jQuery Plugin - Copyright (c) 2012 David Bushell - http://dbushell.com/
* Dual-licensed under the BSD or MIT licenses
*/
        ; (function ($, window, document, undefined) {
            var hasTouch = 'ontouchstart' in document;
            /**
             * Detect CSS pointer-events property
             * events are normally disabled on the dragging element to avoid conflicts
             * https://github.com/ausi/Feature-detection-technique-for-pointer-events/blob/master/modernizr-pointerevents.js
             */
            var hasPointerEvents = (function () {
                var el = document.createElement('div'),
                    docEl = document.documentElement;
                if (!('pointerEvents' in el.style)) {
                    return false;
                }
                el.style.pointerEvents = 'auto';
                el.style.pointerEvents = 'x';
                docEl.appendChild(el);
                var supports = window.getComputedStyle && window.getComputedStyle(el, '').pointerEvents === 'auto';
                docEl.removeChild(el);
                return !!supports;
            })();
            var defaults = {
                listNodeName: 'ol',
                itemNodeName: 'li',
                rootClass: 'dd',
                listClass: 'dd-list',
                itemClass: 'dd-item',
                dragClass: 'dd-dragel',
                handleClass: 'dd-handle',
                collapsedClass: 'dd-collapsed',
                placeClass: 'dd-placeholder',
                noDragClass: 'dd-nodrag',
                emptyClass: 'dd-empty',
                expandBtnHTML: '<button data-action="expand" type="button">Expand</button>',
                collapseBtnHTML: '<button data-action="collapse" type="button">Collapse</button>',
                group: 0,
                maxDepth: 5,
                threshold: 20
            };
            function Plugin(element, options) {
                this.w = $(document);
                this.el = $(element);
                this.options = $.extend({}, defaults, options);
                this.init();
            }
            Plugin.prototype = {
                init: function () {
                    var list = this;
                    list.reset();
                    list.el.data('nestable-group', this.options.group);
                    list.placeEl = $('<div class="' + list.options.placeClass + '"/>');
                    $.each(this.el.find(list.options.itemNodeName), function (k, el) {
                        list.setParent($(el));
                    });
                    list.el.on('click', 'button', function (e) {
                        if (list.dragEl) {
                            return;
                        }
                        var target = $(e.currentTarget),
                            action = target.data('action'),
                            item = target.parent(list.options.itemNodeName);
                        if (action === 'collapse') {
                            list.collapseItem(item);
                        }
                        if (action === 'expand') {
                            list.expandItem(item);
                        }
                    });
                    var onStartEvent = function (e) {
                        var handle = $(e.target);
                        if (!handle.hasClass(list.options.handleClass)) {
                            if (handle.closest('.' + list.options.noDragClass).length) {
                                return;
                            }
                            handle = handle.closest('.' + list.options.handleClass);
                        }
                        if (!handle.length || list.dragEl) {
                            return;
                        }
                        list.isTouch = /^touch/.test(e.type);
                        if (list.isTouch && e.touches.length !== 1) {
                            return;
                        }
                        e.preventDefault();
                        list.dragStart(e.touches ? e.touches[0] : e);
                    };
                    var onMoveEvent = function (e) {
                        if (list.dragEl) {
                            e.preventDefault();
                            list.dragMove(e.touches ? e.touches[0] : e);
                        }
                    };
                    var onEndEvent = function (e) {
                        if (list.dragEl) {
                            e.preventDefault();
                            list.dragStop(e.touches ? e.touches[0] : e);
                        }
                    };
                    if (hasTouch) {
                        list.el[0].addEventListener('touchstart', onStartEvent, false);
                        window.addEventListener('touchmove', onMoveEvent, false);
                        window.addEventListener('touchend', onEndEvent, false);
                        window.addEventListener('touchcancel', onEndEvent, false);
                    }
                    list.el.on('mousedown', onStartEvent);
                    list.w.on('mousemove', onMoveEvent);
                    list.w.on('mouseup', onEndEvent);
                },
                serialize: function () {
                    var data,
                        depth = 0,
                        list = this;
                    step = function (level, depth) {
                        var array = [],
                            items = level.children(list.options.itemNodeName);
                        items.each(function () {
                            var li = $(this),
                                item = $.extend({}, li.data()),
                                sub = li.children(list.options.listNodeName);
                            if (sub.length) {
                                item.children = step(sub, depth + 1);
                            }
                            array.push(item);
                        });
                        return array;
                    };
                    data = step(list.el.find(list.options.listNodeName).first(), depth);
                    return data;
                },
                serialise: function () {
                    return this.serialize();
                },
                reset: function () {
                    this.mouse = {
                        offsetX: 0,
                        offsetY: 0,
                        startX: 0,
                        startY: 0,
                        lastX: 0,
                        lastY: 0,
                        nowX: 0,
                        nowY: 0,
                        distX: 0,
                        distY: 0,
                        dirAx: 0,
                        dirX: 0,
                        dirY: 0,
                        lastDirX: 0,
                        lastDirY: 0,
                        distAxX: 0,
                        distAxY: 0
                    };
                    this.isTouch = false;
                    this.moving = false;
                    this.dragEl = null;
                    this.dragRootEl = null;
                    this.dragDepth = 0;
                    this.hasNewRoot = false;
                    this.pointEl = null;
                },
                expandItem: function (li) {
                    li.removeClass(this.options.collapsedClass);
                    li.children('[data-action="expand"]').hide();
                    li.children('[data-action="collapse"]').show();
                    li.children(this.options.listNodeName).show();
                },
                collapseItem: function (li) {
                    var lists = li.children(this.options.listNodeName);
                    if (lists.length) {
                        li.addClass(this.options.collapsedClass);
                        li.children('[data-action="collapse"]').hide();
                        li.children('[data-action="expand"]').show();
                        li.children(this.options.listNodeName).hide();
                    }
                },
                expandAll: function () {
                    var list = this;
                    list.el.find(list.options.itemNodeName).each(function () {
                        list.expandItem($(this));
                    });
                },
                collapseAll: function () {
                    var list = this;
                    list.el.find(list.options.itemNodeName).each(function () {
                        list.collapseItem($(this));
                    });
                },
                setParent: function (li) {
                    if (li.children(this.options.listNodeName).length) {
                        li.prepend($(this.options.expandBtnHTML));
                        li.prepend($(this.options.collapseBtnHTML));
                    }
                    li.children('[data-action="expand"]').hide();
                },
                unsetParent: function (li) {
                    li.removeClass(this.options.collapsedClass);
                    li.children('[data-action]').remove();
                    li.children(this.options.listNodeName).remove();
                },
                dragStart: function (e) {
                    var mouse = this.mouse,
                        target = $(e.target),
                        dragItem = target.closest(this.options.itemNodeName);
                    this.placeEl.css('height', dragItem.height());
                    mouse.offsetX = e.offsetX !== undefined ? e.offsetX : e.pageX - target.offset().left;
                    mouse.offsetY = e.offsetY !== undefined ? e.offsetY : e.pageY - target.offset().top;
                    mouse.startX = mouse.lastX = e.pageX;
                    mouse.startY = mouse.lastY = e.pageY;
                    this.dragRootEl = this.el;
                    this.dragEl = $(document.createElement(this.options.listNodeName)).addClass(this.options.listClass + ' ' + this.options.dragClass);
                    this.dragEl.css('width', dragItem.width());
                    dragItem.after(this.placeEl);
                    dragItem[0].parentNode.removeChild(dragItem[0]);
                    dragItem.appendTo(this.dragEl);
                    $(document.body).append(this.dragEl);
                    this.dragEl.css({
                        'left': e.pageX - mouse.offsetX,
                        'top': e.pageY - mouse.offsetY
                    });
                    // total depth of dragging item
                    var i, depth,
                        items = this.dragEl.find(this.options.itemNodeName);
                    for (i = 0; i < items.length; i++) {
                        depth = $(items[i]).parents(this.options.listNodeName).length;
                        if (depth > this.dragDepth) {
                            this.dragDepth = depth;
                        }
                    }
                },
                dragStop: function (e) {
                    var el = this.dragEl.children(this.options.itemNodeName).first();
                    el[0].parentNode.removeChild(el[0]);
                    this.placeEl.replaceWith(el);
                    this.dragEl.remove();
                    this.el.trigger('change');
                    if (this.hasNewRoot) {
                        this.dragRootEl.trigger('change');
                    }
                    this.reset();
                },
                dragMove: function (e) {
                    var list, parent, prev, next, depth,
                        opt = this.options,
                        mouse = this.mouse;
                    this.dragEl.css({
                        'left': e.pageX - mouse.offsetX,
                        'top': e.pageY - mouse.offsetY
                    });
                    // mouse position last events
                    mouse.lastX = mouse.nowX;
                    mouse.lastY = mouse.nowY;
                    // mouse position this events
                    mouse.nowX = e.pageX;
                    mouse.nowY = e.pageY;
                    // distance mouse moved between events
                    mouse.distX = mouse.nowX - mouse.lastX;
                    mouse.distY = mouse.nowY - mouse.lastY;
                    // direction mouse was moving
                    mouse.lastDirX = mouse.dirX;
                    mouse.lastDirY = mouse.dirY;
                    // direction mouse is now moving (on both axis)
                    mouse.dirX = mouse.distX === 0 ? 0 : mouse.distX > 0 ? 1 : -1;
                    mouse.dirY = mouse.distY === 0 ? 0 : mouse.distY > 0 ? 1 : -1;
                    // axis mouse is now moving on
                    var newAx = Math.abs(mouse.distX) > Math.abs(mouse.distY) ? 1 : 0;
                    // do nothing on first move
                    if (!mouse.moving) {
                        mouse.dirAx = newAx;
                        mouse.moving = true;
                        return;
                    }
                    // calc distance moved on this axis (and direction)
                    if (mouse.dirAx !== newAx) {
                        mouse.distAxX = 0;
                        mouse.distAxY = 0;
                    } else {
                        mouse.distAxX += Math.abs(mouse.distX);
                        if (mouse.dirX !== 0 && mouse.dirX !== mouse.lastDirX) {
                            mouse.distAxX = 0;
                        }
                        mouse.distAxY += Math.abs(mouse.distY);
                        if (mouse.dirY !== 0 && mouse.dirY !== mouse.lastDirY) {
                            mouse.distAxY = 0;
                        }
                    }
                    mouse.dirAx = newAx;
                    /**
                     * move horizontal
                     */
                    if (mouse.dirAx && mouse.distAxX >= opt.threshold) {
                        // reset move distance on x-axis for new phase
                        mouse.distAxX = 0;
                        prev = this.placeEl.prev(opt.itemNodeName);
                        // increase horizontal level if previous sibling exists and is not collapsed
                        if (mouse.distX > 0 && prev.length && !prev.hasClass(opt.collapsedClass)) {
                            // cannot increase level when item above is collapsed
                            list = prev.find(opt.listNodeName).last();
                            // check if depth limit has reached
                            depth = this.placeEl.parents(opt.listNodeName).length;
                            if (depth + this.dragDepth <= opt.maxDepth) {
                                // create new sub-level if one doesn't exist
                                if (!list.length) {
                                    list = $('<' + opt.listNodeName + '/>').addClass(opt.listClass);
                                    list.append(this.placeEl);
                                    prev.append(list);
                                    this.setParent(prev);
                                } else {
                                    // else append to next level up
                                    list = prev.children(opt.listNodeName).last();
                                    list.append(this.placeEl);
                                }
                            }
                        }
                        // decrease horizontal level
                        if (mouse.distX < 0) {
                            // we can't decrease a level if an item preceeds the current one
                            next = this.placeEl.next(opt.itemNodeName);
                            if (!next.length) {
                                parent = this.placeEl.parent();
                                this.placeEl.closest(opt.itemNodeName).after(this.placeEl);
                                if (!parent.children().length) {
                                    this.unsetParent(parent.parent());
                                }
                            }
                        }
                    }
                    var isEmpty = false;
                    // find list item under cursor
                    if (!hasPointerEvents) {
                        this.dragEl[0].style.visibility = 'hidden';
                    }
                    this.pointEl = $(document.elementFromPoint(e.pageX - document.body.scrollLeft, e.pageY - (window.pageYOffset || document.documentElement.scrollTop)));
                    if (!hasPointerEvents) {
                        this.dragEl[0].style.visibility = 'visible';
                    }
                    if (this.pointEl.hasClass(opt.handleClass)) {
                        this.pointEl = this.pointEl.parent(opt.itemNodeName);
                    }
                    if (this.pointEl.hasClass(opt.emptyClass)) {
                        isEmpty = true;
                    }
                    else if (!this.pointEl.length || !this.pointEl.hasClass(opt.itemClass)) {
                        return;
                    }
                    // find parent list of item under cursor
                    var pointElRoot = this.pointEl.closest('.' + opt.rootClass),
                        isNewRoot = this.dragRootEl.data('nestable-id') !== pointElRoot.data('nestable-id');
                    /**
                     * move vertical
                     */
                    if (!mouse.dirAx || isNewRoot || isEmpty) {
                        // check if groups match if dragging over new root
                        if (isNewRoot && opt.group !== pointElRoot.data('nestable-group')) {
                            return;
                        }
                        // check depth limit
                        depth = this.dragDepth - 1 + this.pointEl.parents(opt.listNodeName).length;
                        if (depth > opt.maxDepth) {
                            return;
                        }
                        var before = e.pageY < (this.pointEl.offset().top + this.pointEl.height() / 2);
                        parent = this.placeEl.parent();
                        // if empty create new list to replace empty placeholder
                        if (isEmpty) {
                            list = $(document.createElement(opt.listNodeName)).addClass(opt.listClass);
                            list.append(this.placeEl);
                            this.pointEl.replaceWith(list);
                        }
                        else if (before) {
                            this.pointEl.before(this.placeEl);
                        }
                        else {
                            this.pointEl.after(this.placeEl);
                        }
                        if (!parent.children().length) {
                            this.unsetParent(parent.parent());
                        }
                        if (!this.dragRootEl.find(opt.itemNodeName).length) {
                            this.dragRootEl.append('<div class="' + opt.emptyClass + '"/>');
                        }
                        // parent root list has changed
                        if (isNewRoot) {
                            this.dragRootEl = pointElRoot;
                            this.hasNewRoot = this.el[0] !== this.dragRootEl[0];
                        }
                    }
                }
            };
            $.fn.nestable = function (params) {
                var lists = this,
                    retval = this;
                lists.each(function () {
                    var plugin = $(this).data("nestable");
                    if (!plugin) {
                        $(this).data("nestable", new Plugin(this, params));
                        $(this).data("nestable-id", new Date().getTime());
                    } else {
                        if (typeof params === 'string' && typeof plugin[params] === 'function') {
                            retval = plugin[params]();
                        }
                    }
                });
                return retval || lists;
            };
        })(window.jQuery || window.Zepto, window, document);
        /*my scripts*/
        $('.dd').nestable('serialize');
        $('.viewlist').on('click', function () {
            $('ol.kanban').addClass('list')
            $('ol.list').removeClass('kanban')
            $('menu').addClass('list')
            $('menu').removeClass('kanban')
        });
        $('.viewkanban').on('click', function () {
            $('ol.list').addClass('kanban')
            $('ol.kanban').removeClass('list')
            $('menu').addClass('kanban')
            $('menu').removeClass('list')
        });
        /*colors*/
        $('#color').spectrum({
            color: "#f00",
            change: function (color) {
                $("#label").text("change called: " + color.toHexString());
            }
        });
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#update').click(saveData);
        });

    </script>

    <script type="text/javascript">
        // sends task data to backend to be stored in database
        function saveData() {
            var str = JSON.stringify(taskList);
            console.log(str);
            $.ajax({
                type: "POST",
                url: "/Projects/Tasks.aspx/ParseTaskData",
                data: JSON.stringify({ "tasksData": taskList }),
                contentType: "application/json; charset=utf-8",
                complete: function () {
                    //alert("success");
                },
                failure: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert("Status: " + textStatus); alert("Error: " + errorThrown);
                }
            });
        }
    </script>

    <script type="text/javascript">
        // stores most currently saved form of task data
        var taskList = [];
        // scrapes and returns the data that's currently on the page
        function scrapeTaskPhaseData() {
            var currentTaskList = [];
            var currentPosition = 1;
            $('li.dd-item', $('#content')).each(function (index, element) {
                currentTaskList[index] = { phase: "", task: "" };
                currentTaskList[index]['phase'] = $(element).parent().children('h3').contents().filter(function () { return this.nodeType == 3 }).text().trim();
                currentTaskList[index]['task'] = $(element).children('h3').children('b').text().trim();
                if (index > 0 && (currentTaskList[index]['phase'] != currentTaskList[index - 1]['phase'])) {
                    currentPosition = 1;
                }
                currentTaskList[index]['position'] = currentPosition;
                currentTaskList[index]['employee'] = $(element).children('div').children('b').text().trim();
                currentPosition++;
            });
            return currentTaskList;
        }
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            // Configure to save every 10 seconds
            window.setInterval(function () {
                if (isChanged) {
                    taskList = scrapeTaskPhaseData();
                    saveData();
                }
            }, 10000);
        });
    </script>

    <script type="text/javascript">
        // scrape task data on load
        window.onload = function () {
            taskList = scrapeTaskPhaseData();
            console.log(JSON.stringify(taskList));
        };
    </script>

    <script type="text/javascript">
        // every second check if taskList == currentTaskList
        var isChanged = false;
        $(document).ready(function () {
            window.setInterval(function () {
                taskListString = JSON.stringify(taskList);
                currentString = JSON.stringify(scrapeTaskPhaseData());
                //console.log(taskListString);
                //console.log(currentString);
                isChanged = !(taskListString == currentString);
                if (isChanged) {
                    document.getElementById("saveNotif").innerHTML = "Not Saved";
                }
                else {
                    document.getElementById("saveNotif").innerHTML = "Auto Saved";
                }
            }, 1000);
        });
    </script>

    <script type="text/javascript">
        window.onbeforeunload = function () {
            if (isChanged) {
                taskList = scrapeTaskPhaseData();
                saveData();
                alert("Changes were saved");
            }
        }
    </script>
</body>
</html>

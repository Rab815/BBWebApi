﻿/*!@license
* Infragistics.Web.ClientUI Pivot Shared localization resources 13.1.20131.2292
*
* Copyright (c) 2011-2013 Infragistics Inc.
*
* http://www.infragistics.com/
*
*/
$.ig=$.ig||{};if(!$.ig.PivotShared){$.ig.PivotShared={};$.extend($.ig.PivotShared,{locale:{invalidDataSource:"The passed data source either null or not supported.",measureList:"Measures",ok:"OK",cancel:"Cancel",addToMeasures:"Add to Measures",addToFilters:"Add to Filters",addToColumns:"Add to Columns",addToRows:"Add to Rows"}})}/*!@license
* Infragistics.Web.ClientUI Pivot Shared 13.1.20131.2292
*
* Copyright (c) 2011-2012 Infragistics Inc.
*
* http://www.infragistics.com/
*
* Depends on: 
*   jquery-1.4.4.js
*   jquery.ui.core.js
*   jquery.ui.widget.js
*   jquery.ui.draggable.js
*   jquery.ui.droppable.js
*   infragistics.util.js
*   infragistics.datasource.js
*   infragistics.olapxmladatasource.js
*   infragistics.olapflatdatasource.js
*   infragistics.templating.js
*   infragistics.ui.shared.js
*   infragistics.ui.scroll.js
*   infragistics.ui.tree.js
*/
(function($){var _aNull=function(val){return val===null||val===undefined},_isInstance=function(object,type){return type!==undefined&&object instanceof type},_draggable=$.ui.draggable.prototype.widgetFullName||$.ui.draggable.prototype.widgetName,_tree=$.ui.igTree.prototype.widgetFullName||$.ui.igTree.prototype.widgetName;$.ig=$.ig||{};$.ig.Pivot=$.ig.Pivot||{};$.ig.Pivot._pivotShared=$.ig.Pivot._pivotShared||{_const:{index:0,dragCursorAt:{top:-10,left:10},dragHelperMarkup:"<div class='ui-widget ui-corner-all ui-igpivot-draghelper'><p><span></span><strong>{0}</strong></p></div>",touchEvents:{mousedown:"touchstart",mouseover:"mouseover",mouseout:"mouseover"},ie:!!/msie [\w.]+/.exec(navigator.userAgent.toLowerCase())},_insertIndex:0,_createDataSource:function(dataSource,dataSourceOptions){var ds=null,dsOptions;if(!_aNull(dataSource)&&(_isInstance(dataSource,$.ig.OlapXmlaDataSource)||_isInstance(dataSource,$.ig.OlapFlatDataSource))){ds=dataSource}else if(!_aNull(dataSourceOptions)){dsOptions=$.extend({},dataSourceOptions);delete dsOptions.xmlaOptions;delete dsOptions.flatDataOptions;if(!_aNull(dataSourceOptions.xmlaOptions)&&!_aNull(dataSourceOptions.xmlaOptions.serverUrl)){dsOptions=$.extend(dsOptions,dataSourceOptions.xmlaOptions);ds=new $.ig.OlapXmlaDataSource(dsOptions)}else if(!_aNull(dataSourceOptions.flatDataOptions)&&(!_aNull(dataSourceOptions.flatDataOptions.dataSource)||!_aNull(dataSourceOptions.flatDataOptions.dataSourceUrl))){dsOptions=$.extend(dsOptions,dataSourceOptions.flatDataOptions);ds=new $.ig.OlapFlatDataSource(dsOptions)}}return ds},_isTouch:function(){var isTouch=typeof window.Modernizr==="object"&&window.Modernizr.touch===true;if(!isTouch||navigator.appVersion.indexOf("Windows")!==-1){if(navigator.appVersion.indexOf("Windows")!==-1&&navigator.appVersion.indexOf("Tablet")!==-1){return true}}return false},_getEvent:function(event){if(this._isTouch()){return this._const.touchEvents[event]}return event},_makeDroppable:function(droppable){var $this=this;if(!this._isTouch()){droppable.droppable({tolerance:"pointer",accept:function(draggable){return draggable.hasClass("ui-igpivot-dragover")===false&&draggable.closest("li.ui-igtree-node").length===0&&(draggable.hasClass("ui-igpivot-metadataitem")||draggable.find(".ui-igpivot-metadataitem:first").length>0)},over:function(event,ui){var isValid=true,markup=$(ui.helper.html()),element=ui.draggable,typeName,name;if(!element.hasClass("ui-igpivot-metadataitem")){element=element.find(".ui-igpivot-metadataitem:first")}if(element.length>0){typeName=element.attr("data-type");name=element.attr("data-name")}else{return false}if($.isFunction($this.options.customMoveValidation)){isValid=$this.options.customMoveValidation.call($this.element,$this.widgetName,typeName,name)}if(ui.helper.hasClass("ui-igpivot-draghelper")){if(isValid){markup.find("span").removeClass("ui-icon-plus ui-icon-cancel").addClass("ui-icon-close").siblings("strong");ui.helper.removeClass($this.css.dropIndicator).addClass($this.css.invalidDropIndicator).html(markup)}else{markup.find("span").removeClass("ui-icon-plus ui-icon-close").addClass("ui-icon-cancel").siblings("strong");ui.helper.removeClass($this.css.dropIndicator).addClass($this.css.invalidDropIndicator).html(markup)}}},out:function(event,ui){var markup=$(ui.helper.html());if(ui.helper.hasClass("ui-igpivot-draghelper")){markup.find("span").removeClass("ui-icon-close ui-icon-plus").addClass("ui-icon-cancel").siblings("strong");ui.helper.removeClass($this.css.dropIndicator).addClass($this.css.invalidDropIndicator).html(markup)}},drop:function(event,ui){var element=ui.draggable,isValid=true,typeName,type,name,item,noCancel;ui.draggable.unbind("."+$this.widgetName);if(!element.hasClass("ui-igpivot-metadataitem")){element=element.find(".ui-igpivot-metadataitem:first")}if(element.length>0){typeName=element.attr("data-type");name=element.attr("data-name")}else{return false}if($.isFunction($this.options.customMoveValidation)){isValid=$this.options.customMoveValidation.call($this.element,$(this).attr("data-role"),typeName,name)}if(!isValid){return false}switch(typeName){case $.ig.Dimension.prototype.getType().typeName():type=$.ig.Dimension.prototype.getType();break;case $.ig.Hierarchy.prototype.getType().typeName():type=$.ig.Hierarchy.prototype.getType();break;case $.ig.Measure.prototype.getType().typeName():type=$.ig.Measure.prototype.getType();break;case $.ig.MeasureList.prototype.getType().typeName():type=$.ig.MeasureList.prototype.getType();break;default:return false}item=$this._ds.getCoreElement(function(el){return el.uniqueName()===name},type);if(item){noCancel=$this._triggerMetadataRemoving(event,element,item);if(noCancel){$this._ds.removeFilterItem(item);$this._ds.removeRowItem(item);$this._ds.removeColumnItem(item);$this._ds.removeMeasureItem(item);$this._triggerMetadataRemoved(event,item);$this._updateDataSource();return true}}return false}})}},_createDropAreaOptions:function(){var $this=this,dropAreaOptions={greedy:true,tolerance:"pointer",activeClass:this.css.activeDropArea,accept:function(draggable){return $this._accept($(this),draggable)},over:function(event,ui){$this._onDraggableOver(event,ui)},out:function(event,ui){$this._onDraggableOut(event,ui)},drop:function(event,ui){var element=ui.draggable,type,name;if(!element.hasClass("ui-igpivot-metadataitem")){element=element.find(".ui-igpivot-metadataitem:first")}if(element.length>0){type=element.attr("data-type");name=element.attr("data-name");if(!name){name="null"}return $this._onDrop(event,ui,$(this),element,type,name)}return false}};return dropAreaOptions},_onDataSourceCollectionChanged:function(collection,collectionChangedArgs,dropArea,isDisabled){var action,items,i,length,name,filter,startingIndex,previousItem,destroyDraggable;action=collectionChangedArgs.action();switch(action){case $.ig.NotifyCollectionChangedAction.prototype.add:items=collectionChangedArgs.newItems().__inner;startingIndex=collectionChangedArgs.newStartingIndex();if(startingIndex===0){this._createMetadataElement(items[0],isDisabled,"prependTo",dropArea)}else{previousItem=dropArea.find(".ui-igpivot-metadataitem")[startingIndex-1];this._createMetadataElement(items[0],isDisabled,"insertAfter",previousItem)}break;case $.ig.NotifyCollectionChangedAction.prototype.remove:items=collectionChangedArgs.oldItems().__inner;filter=function(ind,itemElement){return $(itemElement).attr("data-name")===name};destroyDraggable=function(ind,el){var draggable=$(el).data(_draggable);if(draggable){draggable.destroy()}};for(i=0,length=items.length;i<length;i++){if(items[i]instanceof $.ig.MeasureList){dropArea.find(".ui-igpivot-metadataitem[data-type="+$.ig.MeasureList.prototype.getType().typeName()+"]").each(destroyDraggable).remove()}else{name=items[i].uniqueName();dropArea.find(".ui-igpivot-metadataitem").filter(filter).each(destroyDraggable).remove()}}break;case $.ig.NotifyCollectionChangedAction.prototype.reset:destroyDraggable=function(ind,el){var draggable=$(el).data(_draggable);if(draggable){draggable.destroy()}};dropArea.find(".ui-igpivot-metadataitem").each(destroyDraggable).remove();break}},_createMetadataElement:function(item,isDisabled,appendFunc,target){var $this=this,dragAndDropSettings=this.options.dragAndDropSettings,metadataElement,metadataElementMarkup;metadataElementMarkup="<li ";if(item instanceof $.ig.MeasureList&&item.caption()===null){item.caption($.ig.PivotShared.locale.measureList)}else{metadataElementMarkup+="data-name='"+item.uniqueName()+"' "}metadataElementMarkup+="title='"+item.caption()+"' data-type='"+item.getType().typeName()+"'>";if(item instanceof $.ig.Hierarchy&&!isDisabled){metadataElementMarkup+="<span class='ui-icon "+this.css.filterIcon+"'></span>"}metadataElementMarkup+="<span data-role='caption'>"+item.caption()+"</span>";if(!isDisabled){metadataElementMarkup+="<span class='ui-icon ui-icon-close'></span>"}metadataElementMarkup+="</li>";metadataElement=$(metadataElementMarkup).addClass(this.css.metadataItem);metadataElement[appendFunc](target);if(!isDisabled){metadataElement.find("span.ui-icon-pivot-smallfilter").click(function(event){$this._createFilterDropDown(event,this,item);return false});metadataElement.find("span.ui-icon-close").click(function(event){var noCancel=$this._triggerMetadataRemoving(event,metadataElement,item);if(noCancel){$this._ds.removeFilterItem(item);$this._ds.removeRowItem(item);$this._ds.removeColumnItem(item);$this._ds.removeMeasureItem(item);$this._triggerMetadataRemoved(event,item);$this._updateDataSource();return false}return false});if(!this._isTouch()){metadataElement.draggable({appendTo:dragAndDropSettings.appendTo,containment:dragAndDropSettings.containment,opacity:dragAndDropSettings.dragOpacity,zIndex:dragAndDropSettings.zIndex,cursorAt:this._const.dragCursorAt,revert:false,cancel:".ui-icon",helper:function(event){var target=$(event.target).closest(".ui-igpivot-metadataitem").find("span[data-role='caption']"),markup=$($this._const.dragHelperMarkup.replace("{0}",target.text()));markup.addClass($this.css.invalidDropIndicator).find("span").addClass("ui-icon");return markup},start:function(event,ui){return $this._triggerDragStart(event,ui,item)},drag:function(event,ui){return $this._triggerDrag(event,ui,item)},over:function(event,ui){$this._onDraggableOver(event,ui)},out:function(event,ui){$this._onDraggableOut(event,ui)},stop:function(event,ui){$this._triggerDragStop(event,ui)}})}metadataElement.click(function(event){$this._createMetadataItemDropDown(event,this,item)})}return metadataElement},_accept:function(targetElement,draggable){var target,typeName,isValid=false,isMeasureDimension,dimension,dataSource=this._ds;if(!draggable.hasClass("ui-igpivot-metadataitem")){draggable=draggable.find(".ui-igpivot-metadataitem:first")}typeName=draggable.attr("data-type");target=targetElement.attr("data-role");dimension=dataSource.getCoreElement(function(el){return el.dimensionType()===$.ig.DimensionType.prototype.measure},$.ig.Dimension.prototype.getType());isMeasureDimension=draggable.text()===dimension.name()||draggable.text()===dimension.caption();if(typeName){switch(target){case"rows":case"columns":isValid=typeName===$.ig.Hierarchy.prototype.getType().typeName()||typeName===$.ig.Dimension.prototype.getType().typeName()&&!isMeasureDimension||typeName===$.ig.MeasureList.prototype.getType().typeName();break;case"filters":isValid=typeName===$.ig.Hierarchy.prototype.getType().typeName()||typeName===$.ig.Dimension.prototype.getType().typeName()&&!isMeasureDimension;break;case"measures":isValid=(typeName===$.ig.Measure.prototype.getType().typeName()||isMeasureDimension)&&"MeasureList"!==draggable.attr("data-type");break}}return isValid},_onDraggableOver:function(event,ui){var $this=this,isValid=true,markup=$(ui.helper.html()),element=ui.draggable,typeName,name;ui.draggable.addClass("ui-igpivot-dragover");if(!element.hasClass("ui-igpivot-metadataitem")){element=element.find(".ui-igpivot-metadataitem:first")}if(element.length>0){typeName=element.attr("data-type");name=element.attr("data-name")}else{return false}if($.isFunction(this.options.customMoveValidation)){isValid=this.options.customMoveValidation.call(this.element,$(event.target).attr("data-role"),typeName,name)}if(ui.helper.hasClass("ui-igpivot-draghelper")){if(isValid){markup.find("span").removeClass("ui-icon-cancel ui-icon-close").addClass("ui-icon-plus").siblings("strong");ui.helper.removeClass(this.css.invalidDropIndicator).addClass(this.css.dropIndicator).html(markup)}else{markup.find("span").removeClass("ui-icon-plus ui-icon-close").addClass("ui-icon-cancel").siblings("strong");ui.helper.removeClass(this.css.dropIndicator).addClass(this.css.invalidDropIndicator).html(markup)}}if(!isValid){return false}ui.draggable.bind("drag."+this.widgetName,function(event1,ui1){$this._onDraggableDrag(event1,ui1)})},_onDraggableDrag:function(event,ui){var target=$(event.originalEvent.target),insertItem="<li class='"+this.css.insertItem+"'></li>";if(target.hasClass("ui-igpivot-insertitem")){if(!this._const.ie||this._const.ie&&document.documentMode!==8){return}}$(document).find(".ui-igpivot-insertitem").remove();if(target.is("span")){target=target.closest(".ui-igpivot-metadataitem")}if(!(target.parent().hasClass("ui-igpivot-droparea")||target.hasClass("ui-igpivot-droparea"))){return}if(target.is(".ui-igpivot-metadataitem")){if(this._shouldAppendToTarget(target,ui)){this._insertIndex=target.index()+1;$(insertItem).insertAfter(target)}else{if(target.index()===0){this._insertIndex=0;$(insertItem).insertBefore(target)}else{this._insertIndex=target.index();$(insertItem).insertBefore(target)}}}else if(target.is(".ui-igpivot-droparea")){target=target.find(".ui-igpivot-metadataitem:last");this._insertIndex=target.index()+1;$(insertItem).insertAfter(target)}},_onDraggableOut:function(event,ui){var markup=$(ui.helper.html()),invalidIcon;ui.draggable.removeClass("ui-igpivot-dragover");invalidIcon=ui.draggable.closest("li.ui-igtree-node").length===0?"ui-icon-close":"ui-icon-cancel";if(ui.helper.hasClass("ui-igpivot-draghelper")){markup.find("span").removeClass("ui-icon-plus").addClass(invalidIcon).siblings("strong");ui.helper.removeClass(this.css.dropIndicator).addClass(this.css.invalidDropIndicator).html(markup)}ui.draggable.unbind("drag."+this.widgetName);$(document).find(".ui-igpivot-insertitem").remove();this._insertIndex=0},_onDrop:function(event,ui,targetElement,draggedElement,typeName,name){var dataSource=this._ds,isValid=true,targetRole=$(targetElement).attr("data-role"),targetIndex=this._insertIndex,item,type,sourceRole,sourceIndex,filterMembers,i,noCancel,firstHierarchy,dimensionName;ui.draggable.unbind("."+this.widgetName);$(document).find(".ui-igpivot-insertitem").remove();if($.isFunction(this.options.customMoveValidation)){isValid=this.options.customMoveValidation.call(this.element,$(event.target).attr("data-role"),typeName,name)}if(!isValid){return false}switch(typeName){case $.ig.Dimension.prototype.getType().typeName():type=$.ig.Dimension.prototype.getType();break;case $.ig.Hierarchy.prototype.getType().typeName():type=$.ig.Hierarchy.prototype.getType();break;case $.ig.Measure.prototype.getType().typeName():type=$.ig.Measure.prototype.getType();break;case $.ig.MeasureList.prototype.getType().typeName():type=$.ig.MeasureList.prototype.getType();break;default:return false}dimensionName=name.substr(1,name.length-2);if(null!==dataSource.getDimension(name)&&$.ig.DimensionType.prototype.measure===dataSource.getDimension(name).dimensionType()){for(i=dataSource.metadataTree().children().length-1;i>=0;i--){if(dimensionName===dataSource.metadataTree().children()[i].caption()){firstHierarchy=dataSource.metadataTree().children()[i];while(null!==firstHierarchy.children()){firstHierarchy=firstHierarchy.children()[0]}name=firstHierarchy.item().uniqueName();item=dataSource.getMeasure(name);break}}}else if(typeName===$.ig.Dimension.prototype.getType().typeName()){for(i=dataSource.metadataTree().children().length-1;i>=0;i--){if(dimensionName===dataSource.metadataTree().children()[i].caption()||dimensionName===dataSource.metadataTree().children()[i].caption().replace(" ","")){if(dataSource.metadataTree().children()[i].item().defaultHierarchy()){name=dataSource.metadataTree().children()[i].item().defaultHierarchy()}else if(null!==dataSource.metadataTree().children()[i].children()[0].item()){name=dataSource.metadataTree().children()[i].children()[0].item().uniqueName()}else{firstHierarchy=dataSource.metadataTree().children()[i].children()[0];while(null===firstHierarchy.item()){firstHierarchy=firstHierarchy.children()[0]}name=firstHierarchy.item().uniqueName()}break}}item=dataSource.getHierarchy(name)}else{item=dataSource.getCoreElement(function(el){return el.uniqueName()===name},type)}if(!item){return false}noCancel=this._triggerMetadataDropping(event,ui,targetElement,draggedElement,item,targetIndex);if(noCancel){if((sourceIndex=$.inArray(item,dataSource.filters()))>-1){sourceRole="filters"}else if((sourceIndex=$.inArray(item,dataSource.rowAxis()))>-1){sourceRole="rows"}else if((sourceIndex=$.inArray(item,dataSource.columnAxis()))>-1){sourceRole="columns"}else if((sourceIndex=$.inArray(item,dataSource.measures()))>-1){sourceRole="measures"}else{sourceRole=null}if(sourceRole!==null&&sourceRole===targetRole&&sourceIndex<targetIndex){targetIndex--}if(item instanceof $.ig.MeasureList){dataSource.setMeasureListLocation(targetRole);dataSource.setMeasureListIndex(targetIndex)}else{if(item instanceof $.ig.Hierarchy){filterMembers=dataSource.getFilterMemberNames(name)}switch(sourceRole){case"filters":dataSource.removeFilterItem(item);break;case"rows":dataSource.removeRowItem(item);break;case"columns":dataSource.removeColumnItem(item);break;case"measures":dataSource.removeMeasureItem(item);break}switch(targetRole){case"filters":dataSource.insertFilterItem(targetIndex,item);break;case"rows":dataSource.insertRowItem(targetIndex,item);break;case"columns":dataSource.insertColumnItem(targetIndex,item);break;case"measures":dataSource.insertMeasureItem(targetIndex,item);break}if(item instanceof $.ig.Hierarchy){for(i=0;i<filterMembers.length;i++){dataSource.addFilterMember(name,filterMembers[i])}}}if(this.widgetName==="igPivotGrid"&&ui.draggable.data(_draggable)){delete ui.draggable.data(_draggable).plugins.stop}this._updateDataSource();this._triggerMetadataDropped(event,ui,targetElement,draggedElement,item,targetIndex);return true}return false},_createMetadataItemDropDown:function(event,targetElement,metadataItem){var $this=this,options=this.options,closestDropArea,dropDownParent,dropDownElement,menu,addMeasureList,addHierarchy,items,customValidation;closestDropArea=$(targetElement).closest(".ui-igpivot-droparea").attr("data-role");if($.isFunction(this.options.customMoveValidation)){customValidation=function(location){return $this.options.customMoveValidation.call($this.element,location,metadataItem.getType().name,metadataItem.uniqueName()||undefined)}}else{customValidation=function(){return true}}dropDownParent=$(this.options.dropDownParent).first();dropDownElement=$("<div class='"+this.css.metadataItemDropDown+"'></div>");dropDownElement.data("efh","1");dropDownElement.appendTo(dropDownParent).bind(this._getEvent("mousedown"),function(event1){event1.stopPropagation()});menu=$("<ul class='ui-widget'></ul>").appendTo(dropDownElement);if(metadataItem instanceof $.ig.Measure){if(!options.disableMeasuresDropArea&&closestDropArea!=="measures"&&customValidation("measures")){$("<li><span class='ui-icon ui-icon-pivot-measures'></span>"+$.ig.PivotShared.locale.addToMeasures+"</li>").appendTo(menu).click(function(){$this._ds.removeMeasureItem(metadataItem);$this._ds.addMeasureItem(metadataItem);dropDownElement.remove();$this._updateDataSource()})}}else if(metadataItem instanceof $.ig.MeasureList){addMeasureList=function(location,index){$this._ds.setMeasureListLocation(location);$this._ds.setMeasureListIndex(index);dropDownElement.remove();$this._updateDataSource()};if(!options.disableColumnsDropArea&&closestDropArea!=="columns"&&customValidation("columns")){$("<li><span class='ui-icon ui-icon-pivot-clumns'></span>"+$.ig.PivotShared.locale.addToColumns+"</li>").appendTo(menu).click(function(){addMeasureList("columns",$this._ds.columnAxis().length)})}if(!options.disableRowsDropArea&&closestDropArea!=="rows"&&customValidation("rows")){$("<li><span class='ui-icon ui-icon-pivot-rows'></span>"+$.ig.PivotShared.locale.addToRows+"</li>").appendTo(menu).click(function(){addMeasureList("rows",$this._ds.rowAxis().length)})}}else{addHierarchy=function(addMethod){var i,name=metadataItem.uniqueName(),filterMembers=$this._ds.getFilterMemberNames(name);$this._ds.removeFilterItem(metadataItem);$this._ds.removeColumnItem(metadataItem);$this._ds.removeRowItem(metadataItem);$this._ds[addMethod](metadataItem);for(i=0;i<filterMembers.length;i++){$this._ds.addFilterMember(name,filterMembers[i])}dropDownElement.remove();$this._updateDataSource()};if(!options.disableFiltersDropArea&&closestDropArea!=="filters"&&customValidation("filters")){$("<li><span class='ui-icon ui-icon-pivot-filters'></span>"+$.ig.PivotShared.locale.addToFilters+"</li>").appendTo(menu).click(function(){addHierarchy("addFilterItem")})}if(!options.disableColumnsDropArea&&closestDropArea!=="columns"&&customValidation("columns")){$("<li><span class='ui-icon ui-icon-pivot-columns'></span>"+$.ig.PivotShared.locale.addToColumns+"</li>").appendTo(menu).click(function(){addHierarchy("addColumnItem")})}if(!options.disableRowsDropArea&&closestDropArea!=="rows"&&customValidation("rows")){$("<li><span class='ui-icon ui-icon-pivot-rows'></span>"+$.ig.PivotShared.locale.addToRows+"</li>").appendTo(menu).click(function(){addHierarchy("addRowItem")})}}items=dropDownElement.find("li");if(items.length===0){dropDownElement.remove();return}dropDownElement.css("position","absolute").position({of:targetElement,my:"left top",at:"left bottom"});items.bind(this._getEvent("mouseover"),function(){$(this).addClass("ui-state-hover")}).bind(this._getEvent("mouseout"),function(){$(this).removeClass("ui-state-hover")});$(document).bind(this._getEvent("mousedown")+"."+this.widgetName,function(){dropDownElement.remove();$(document).unbind("."+$this.widgetName)})},_createFilterDropDown:function(event,targetElement,hierarchy){var $this=this,hierarchyName,hierarchyFilterView,dropDownParent,dropDownElement,filterMembersTree,buttonContainer,removeFilterDropDown,noCancel;noCancel=this._triggerFilterDropDownOpening(event,hierarchy);if(noCancel){hierarchyName=hierarchy.uniqueName();hierarchyFilterView=new $.ig.HierarchyFilterView(hierarchy);dropDownParent=$(this.options.dropDownParent).first();dropDownElement=$("<div class='"+this.css.filterDropDown+"'></div>");dropDownElement.data("efh","1");dropDownElement.css("position","absolute").attr("data-hierarchy",hierarchyName).appendTo(dropDownParent).position({of:targetElement,my:"left top",at:"left bottom"}).bind(this._getEvent("mousedown"),function(event1){event1.stopPropagation()});filterMembersTree=$("<div class='"+this.css.filterMembers+"'></div>").appendTo(dropDownElement);buttonContainer=$("<div class='ui-igpivot-filterdropdown-buttoncontainer'></div>").appendTo(dropDownElement);removeFilterDropDown=function(event1){$this._removeFilterDropDown(event1,dropDownElement,hierarchy)};$("<button></button>").attr("data-role","ok").text($.ig.PivotShared.locale.ok).appendTo(buttonContainer).igButton().igButton("disable").click(function(event1){$this._onFilterOk(event1,dropDownElement,hierarchyFilterView,hierarchy)});$("<button></button>").attr("data-role","cancel").text($.ig.PivotShared.locale.cancel).appendTo(buttonContainer).igButton().click(removeFilterDropDown);$(document).bind(this._getEvent("mousedown")+"."+this.widgetName,removeFilterDropDown);this._loadFilterMembers(hierarchyFilterView,hierarchy,removeFilterDropDown);this._triggerFilterDropDownOpened(event,dropDownElement,hierarchy)}},_loadFilterMembers:function(hierarchyFilterView,hierarchy,removeFilterDropDown){var $this=this,dataSource,hierarchyName,rootFilterMembers,filterMembers,filterMember,member,members,maxLevel,levels,levelMembers,rootLevel,i;dataSource=this._ds;hierarchyName=hierarchy.uniqueName();filterMembers=dataSource.getFilterMemberNames(hierarchyName);if(filterMembers.length>0){members=[];maxLevel=0;for(i=0;i<filterMembers.length;i++){member=dataSource.tryGetMember(filterMembers[i]);if(member){members.push(member);if(member.levelDepth()>maxLevel){maxLevel=member.levelDepth()}}}levels=dataSource.getCoreElements(function(el){return el.hierarchyUniqueName()===hierarchyName&&el.depth()<=maxLevel},$.ig.Level.prototype.getType());for(i=0;i<levels.length;i++){levelMembers=dataSource.tryGetMembersForLevel(levels[i].uniqueName());hierarchyFilterView.addFiltersForMembers(levelMembers)}rootFilterMembers=hierarchyFilterView.getRootFilterMembers();rootFilterMembers=rootFilterMembers?rootFilterMembers.__inner:[];for(i=0;i<rootFilterMembers.length;i++){rootFilterMembers[i].isSelected(false)}for(i=0;i<members.length;i++){filterMember=hierarchyFilterView.tryGetFilterMember(members[i].uniqueName()).filterMember;if(filterMember){filterMember.isSelected(true)}}}rootLevel=dataSource.getCoreElement(function(el){return el.depth()===0&&el.hierarchyUniqueName()===hierarchyName},$.ig.Level.prototype.getType());this._ds.getMembersOfLevel(rootLevel.uniqueName()).done(function(m){$this._onFilterMembersLoaded(hierarchyFilterView,m,hierarchy)}).fail(removeFilterDropDown)},_onFilterMembersLoaded:function(hierarchyFilterView,members,hierarchy){var hierarchyName,dropDownElement,filterMembersTree;hierarchyName=hierarchy.uniqueName();hierarchyFilterView.addFiltersForMembers(members);dropDownElement=$(".ui-igpivot-filterdropdown").filter(function(){return $(this).attr("data-hierarchy")===hierarchyName});if(dropDownElement.length>0){filterMembersTree=$(dropDownElement[0]).find(".ui-igpivot-filtermembers");this._initTree(filterMembersTree,hierarchyFilterView)}},_getScrollBarWidth:function(){var el=$('<div style="width: 100px; height: 100px; position: absolute; top: -10000px; left: -10000px; overflow: scroll"></div>').appendTo($(document.body)),scrollWidth;scrollWidth=el[0].offsetWidth-el[0].clientWidth;el.remove();return scrollWidth},_getElementSize:function(element){var el=$('<div style="width: 5000px; height: 5000px; position: absolute; top: -10000px; left: -10000px;"></div>').appendTo($(document.body)),result,position,float;position=element.css("position");float=element.css("float");element.css({position:"relative","float":"left"});element.appendTo(el);result=[element.width(),element.height()];element.css("position",position);element.css("float",float);element.detach();el.remove();return result},_arrangeDropDown:function(onExpand){var $this=this,fdd,fm,fmHeight,bcHeight,ddTop,bTop,bHeight,bBottom,fmBottom,fmMaxHeight,ddLeft,bLeft,bWidth,bRight,fmRight,fddMaxWidth,parentOffset,elementSize,fddWidth,scrollTop;fdd=$(".ui-igpivot-filterdropdown");fm=$(".ui-igpivot-filterdropdown .ui-igpivot-filtermembers");fmHeight=fm.height();bcHeight=$(".ui-igpivot-filterdropdown-buttoncontainer").height();ddTop=parseInt(fdd.css("top").replace("px",""),10);bTop=$("body").css("top");if(bTop==="auto"){bTop=0}else{bTop=parseInt(bTop.replace("px",""),10)}bHeight=$("body").height();bBottom=bTop+bHeight;fmBottom=bBottom-bcHeight;fmMaxHeight=Math.floor(fmBottom-ddTop);ddLeft=parseInt(fdd.css("left").replace("px",""),10);bLeft=$("body").css("left");if(bLeft==="auto"){bLeft=0}else{bLeft=parseInt(bLeft.replace("px",""),10)}bWidth=$("body").width();bRight=bLeft+bWidth;fmRight=bRight;fddMaxWidth=Math.floor(fmRight-ddLeft);fm.css("max-height",fmMaxHeight);fdd.css("max-width",fddMaxWidth);parentOffset=parseInt(fdd.css("padding-left").replace("px",""),10)+parseInt(fdd.css("padding-right").replace("px",""),10)+parseInt(fdd.css("border-left-width").replace("px",""),10)+parseInt(fdd.css("border-right-width").replace("px",""),10);scrollTop=fm[0].scrollTop;fm.detach();elementSize=$this._getElementSize(fm);fdd.prepend(fm);fm[0].scrollTop=scrollTop;fddWidth=elementSize[0]+$this._getScrollBarWidth()+parseInt(fm.css("padding-right").replace("px",""),10)+parentOffset;fddWidth=Math.max(fddWidth,parseInt(fdd.css("min-width").replace("px",""),10));fdd.css("width",fddWidth);if(fmHeight>fmMaxHeight||onExpand===false){if(fddWidth<=fddMaxWidth){fm.css("overflow-x","hidden")}else{fm.css("overflow-x","auto")}}},_initTree:function(filterMembersTree,hierarchyFilterView){var $this=this,dataSource=this._ds,checkChildNodes,parsedFilterMembers;$(filterMembersTree).siblings(".ui-igpivot-filterdropdown-buttoncontainer").children(".ui-igbutton[data-role=ok]").igButton("enable");checkChildNodes=function(nodeElement,filterMembers){var isSelected,state,i,filterMember;for(i=0;i<filterMembers.length;i++){filterMember=filterMembers[i].filterMember;isSelected=filterMember.isSelected();if(isSelected.hasValue()){if(isSelected.value()){state="on"}else{state="off"}}else{state="partial"}$(nodeElement).find(".ui-igtree-node[data-value='"+filterMembers[i].uniqueName+"']").children("[data-role=checkbox]").attr("data-chk",state).children("span").removeClass("ui-state-disabled ui-igcheckbox-normal-on ui-igcheckbox-normal-partial ui-igcheckbox-normal-off").addClass("ui-igcheckbox-normal-"+state+(state==="partial"?" ui-igcheckbox-normal-on ui-state-disabled":""))}};parsedFilterMembers=this._parseFilterMembers(hierarchyFilterView.getRootFilterMembers());filterMembersTree.igTree({dataSource:parsedFilterMembers,loadOnDemand:true,checkboxMode:"triState",bindings:{textKey:"caption",valueKey:"uniqueName",childDataProperty:"children"},nodeCollapsed:function(event,ui){$this._arrangeDropDown(false)},nodeExpanded:function(event,ui){$this._arrangeDropDown(true)},nodeCheckstateChanged:function(event,ui){var okButton,isSelected;okButton=$(this).siblings(".ui-igpivot-filterdropdown-buttoncontainer").children(".ui-igbutton[data-role=ok]");if(ui.newCheckedNodes&&ui.newCheckedNodes.length>0){okButton.igButton("enable")}else{okButton.igButton("disable")}isSelected=$(ui.node.element).children("[data-role=checkbox]").attr("data-chk");isSelected=isSelected==="partial"?null:isSelected==="on"?true:false;ui.node.data.filterMember.isSelected(isSelected)}}).css("overflow-x","hidden").data(_tree)._executeUrlRequest=function(node,binding,path,key){var $$this=this,nodeData=this.nodeDataFor(node.attr("data-path")),ul,indicator;ul=node.children("ul");$("<li style='width: 20px' data-role='loading'>&nbsp;</li>").appendTo(ul);ul.show();indicator=ul.children("li").igLoading({includeVerticalOffset:false}).data("igLoading").indicator();indicator.show();this._populatingNode={ul:ul,node:node,indicator:indicator};dataSource.getMembersOfMember(nodeData.member.uniqueName()).done(function(members){var parsedFilterMembers1;hierarchyFilterView.addFiltersForMembers(members);parsedFilterMembers1=$this._parseFilterMembers(nodeData.filterMember.children());$$this._populateNodeData(true,"",{data:function(){return parsedFilterMembers1}});$$this._updateParentState(node);checkChildNodes(node,nodeData.children);$this._triggerFilterMembersLoaded(node,parsedFilterMembers,parsedFilterMembers1)})};checkChildNodes(filterMembersTree,parsedFilterMembers);this._triggerFilterMembersLoaded(filterMembersTree,parsedFilterMembers,parsedFilterMembers)},_parseFilterMembers:function(filterMembers){var parsedFilterMembers,parsedFilterMember,filterMember,member,i;if(filterMembers===null){return[]}filterMembers=filterMembers.__inner;parsedFilterMembers=[];for(i=0;i<filterMembers.length;i++){filterMember=filterMembers[i];member=filterMember.member();parsedFilterMember={};parsedFilterMember.filterMember=filterMember;parsedFilterMember.member=member;parsedFilterMember.caption=member.caption();parsedFilterMember.uniqueName=member.uniqueName();parsedFilterMember.children=[];parsedFilterMembers.push(parsedFilterMember)}return parsedFilterMembers},_onFilterOk:function(event,dropDownElement,hierarchyFilterView,hierarchy){var dataSource=this._ds,hierarchyName=hierarchy.uniqueName(),filterMembers,checkedFilterMembers,i,noCancel;filterMembers=hierarchyFilterView.getSelectedFilterItems().__inner;checkedFilterMembers=[];for(i=0;i<filterMembers.length;i++){checkedFilterMembers.push(filterMembers[i].member().uniqueName())}noCancel=this._triggerFilterDropDownOk(event,dropDownElement,hierarchy,checkedFilterMembers);if(noCancel){dataSource.removeAllFilterMembers(hierarchyName);for(i=0;i<checkedFilterMembers.length;i++){dataSource.addFilterMember(hierarchyName,checkedFilterMembers[i])}this._updateDataSource();this._removeFilterDropDown(event,dropDownElement,hierarchy)}},_removeFilterDropDown:function(event,dropDownElement,hierarchy){var noCancel;noCancel=this._triggerFilterDropDownClosing(event,dropDownElement,hierarchy);if(noCancel){dropDownElement.find(".ui-igtree").igTree("destroy");dropDownElement.find(".ui-button").igButton("destroy");dropDownElement.remove();$(document).unbind("."+this.widgetName);this._triggerFilterDropDownClosed(event,hierarchy)
}},_triggerDataSourceInitialized:function(evt,args){this._trigger("dataSourceInitialized",evt,args)},_triggerDataSourceUpdated:function(evt,args){this._trigger("dataSourceUpdated",evt,args)},_triggerDragStart:function(event,ui,item){var args={helper:ui.helper,offset:ui.offset,originalPosition:ui.originalPosition,position:ui.position,metadata:item};return this._trigger("dragStart",event,args)},_triggerDrag:function(event,ui,item){var args={helper:ui.helper,offset:ui.offset,originalPosition:ui.originalPosition,position:ui.position,metadata:item};return this._trigger("drag",event,args)},_triggerDragStop:function(event,ui){this._trigger("dragStop",event,ui)},_triggerMetadataDropping:function(event,ui,targetElement,item,itemIndex){var args={helper:ui.helper,offset:ui.offset,position:ui.position,targetElement:targetElement,metadata:item,metadataIndex:itemIndex};return this._trigger("metadataDropping",event,args)},_triggerMetadataDropped:function(event,ui,targetElement,draggedElement,item,itemIndex){var args={helper:ui.helper,offset:ui.offset,position:ui.position,targetElement:targetElement,draggedElement:draggedElement,metadata:item,metadataIndex:itemIndex};this._trigger("metadataDropped",event,args)},_triggerMetadataRemoving:function(event,targetElement,item){var args={targetElement:targetElement,metadata:item};return this._trigger("metadataRemoving",event,args)},_triggerMetadataRemoved:function(event,item){var args={metadata:item};this._trigger("metadataRemoved",event,args)},_triggerFilterDropDownOpening:function(event,hierarchy){var args={hierarchy:hierarchy};return this._trigger("filterDropDownOpening",event,args)},_triggerFilterDropDownOpened:function(event,dropDownElement,hierarchy){var args={dropDownElement:dropDownElement,hierarchy:hierarchy};this._trigger("filterDropDownOpened",event,args)},_triggerFilterMembersLoaded:function(parent,rootFilterMembers,filterMembers){var args={parent:parent,rootFilterMembers:rootFilterMembers,filterMembers:filterMembers};this._trigger("filterMembersLoaded",null,args)},_triggerFilterDropDownOk:function(event,dropDownElement,hierarchy,filterMembers){var args={dropDownElement:dropDownElement,hierarchy:hierarchy,filterMembers:filterMembers};return this._trigger("filterDropDownOk",event,args)},_triggerFilterDropDownClosing:function(event,dropDownElement,hierarchy){var args={dropDownElement:dropDownElement,hierarchy:hierarchy};return this._trigger("filterDropDownClosing",event,args)},_triggerFilterDropDownClosed:function(event,hierarchy){var args={hierarchy:hierarchy};this._trigger("filterDropDownClosed",event,args)}};$.widget("ui.igOlapDataSourceWidget",{_create:function(){this._ds=$.ig.Pivot._pivotShared._createDataSource(null,this.options.dataSourceOptions)},dataSource:function(){return this._ds}})})(jQuery);
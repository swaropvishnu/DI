<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
	<xsl:output method="xml" encoding="utf-16" indent="yes"/>
	<xsl:template match="/Menus">
		<ul class="sidebar-menu">
			<xsl:call-template name="MenuListing">
			</xsl:call-template>
		</ul>
	</xsl:template>

	<xsl:template name="MenuListing">
		<xsl:apply-templates select="Menu"></xsl:apply-templates>
	</xsl:template>

	<xsl:template match="Menu">
		<xsl:choose>
			<xsl:when test="ParentId >0">

				<li class="treeview">
					
					<a   href="#">
						<!--   Convert Menu child elements to <li> <a> html tags and  add attributes inside <a> tag -->
					
						<xsl:attribute name="href">
							<xsl:value-of select="NavUrl"/>
						</xsl:attribute>
						<xsl:attribute name="Title">
							<xsl:value-of select ="Description"/>							
						</xsl:attribute>
						<!--<i class="fa fa-laptop"></i>-->
						<i class="fa fa-circle-o">
							<span  style="margin-left:16px;"><xsl:value-of select="Text"/>
							</span>
					</i>				
					</a>
					
					<!-- Call MenuListing if there are child Menu nodes -->
					<xsl:if test="count(Menu) > 0">
						<span class="fa fa-angle-left pull-right"></span>
						<ul class="treeview-menu">
							<xsl:call-template name="MenuListing">
							</xsl:call-template>
							
						</ul>
					</xsl:if>

				</li>
			</xsl:when>
			<xsl:otherwise>
				<li  class="treeview">
					
					
					<a  class="fa fa-edit" href="#">
					<xsl:attribute name="href">
						<xsl:value-of select="NavUrl"/>
					</xsl:attribute>
					<xsl:attribute name="Title">
						<xsl:value-of select ="Description"/>
					</xsl:attribute>
						<!--<i> class="fa fa-laptop"></i>-->
											
							
						<span style="margin-left:7px;">
							<xsl:value-of select="Text"/>
							</span>
								
						
					<xsl:if test="count(Menu) > 0">
						<span class="fa fa-angle-left pull-right"></span>
						
				</xsl:if>
						
				</a>

						<!-- Call MenuListing if there are child Menu nodes -->
						<xsl:if test="count(Menu) > 0">
												
							<ul class="treeview-menu">							
							 <xsl:call-template name="MenuListing">
						    </xsl:call-template>
								
							</ul>
						</xsl:if>
				</li>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
</xsl:stylesheet>
﻿const html = document.getElementsByTagName('html')[0]
const sidebar = document.getElementById('sidebar')
const backgroundShadow = document.getElementById('backgroundShadow')
let sidebarIsActive = false; const toggleSidebar = () => {
    sidebarIsActive ? sidebarIsActive = false : sidebarIsActive = true
    sidebarIsActive ? sidebar.classList.replace('hidden', 'flex') : sidebar.classList.replace('flex', 'hidden')
    sidebarIsActive ? backgroundShadow.classList.replace('hidden', 'flex') : backgroundShadow.classList.replace('flex', 'hidden')
    sidebarIsActive ? html.classList.replace('overflow-y-scroll', 'overflow-y-hidden') : html.classList.replace('overflow-y-hidden', 'overflow-y-scroll')
}
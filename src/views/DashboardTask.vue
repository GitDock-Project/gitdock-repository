<template>
  <div class="min-h-screen bg-[#eef2f5] flex items-center justify-center p-4">
    <div class="flex w-full max-w-[1360px] bg-white rounded-3xl shadow-2xl overflow-hidden">

      <!-- Sidebar -->
      <aside class="w-[280px] bg-[#f8fafd] p-7 border-r border-[#e9edf2] flex flex-col gap-6">
        <!-- Logo -->
        <div class="text-2xl font-bold tracking-tight text-[#0b1b33] flex items-center gap-2">
          <span class="w-2 h-2 bg-blue-500 rounded-full"></span>
          TaskFlow
        </div>

        <div class="text-xs font-semibold uppercase tracking-wide text-[#5e6f88] -mt-2">Workspace</div>

        <!-- Search -->
        <div class="bg-white rounded-full px-4 py-2 flex items-center gap-2 border border-[#dee4eb] shadow-sm">
          <i class="fa fa-search text-[#8b9eb5] text-sm"></i>
          <input
            type="text"
            placeholder="Search tasks, projects..."
            class="w-full outline-none text-sm placeholder-[#9aabbf]"
          />
        </div>

        <!-- Assign Task Button -->
        <button
          class="bg-[#1d4ed8] rounded-full py-3 text-white font-semibold text-sm flex items-center justify-center gap-2 shadow-lg shadow-blue-600/25 hover:bg-blue-600"
        >
          <i class="fa fa-plus text-xs"></i> Assign Task
        </button>

        <!-- Four percentage circles -->
        <div class="flex justify-between">
          <div class="flex flex-col items-center">
            <div
              class="w-12 h-12 rounded-full bg-white flex items-center justify-center font-bold text-[#1e293b] border-4 border-t-orange-500 border-l-orange-500 border-r-transparent border-b-transparent"
            >
              12%
            </div>
          </div>
          <div class="flex flex-col items-center">
            <div
              class="w-12 h-12 rounded-full bg-white flex items-center justify-center font-bold text-[#1e293b] border-4 border-r-blue-500 border-b-blue-500 border-l-transparent border-t-transparent"
            >
              5%
            </div>
          </div>
          <div class="flex flex-col items-center">
            <div
              class="w-12 h-12 rounded-full bg-white flex items-center justify-center font-bold text-[#1e293b] border-4 border-t-purple-500 border-r-purple-500 border-l-transparent border-b-transparent"
            >
              24%
            </div>
          </div>
          <div class="flex flex-col items-center">
            <div
              class="w-12 h-12 rounded-full bg-white flex items-center justify-center font-bold text-[#1e293b] border-4 border-l-green-500 border-b-green-500 border-r-transparent border-t-transparent"
            >
              8%
            </div>
          </div>
        </div>

        <!-- Navigation -->
        <nav class="mt-4">
          <ul class="space-y-1">
            <li
              class="px-4 py-3 rounded-xl flex items-center gap-3 text-[#1d4ed8] font-medium cursor-pointer bg-[#e6edf9]"
            >
              <i class="fa fa-tachometer-alt w-5"></i> Dashboard
            </li>
            <li
              class="px-4 py-3 rounded-xl flex items-center gap-3 text-[#40566e] font-medium cursor-pointer hover:bg-gray-100"
            >
              <i class="fa fa-project-diagram w-5"></i> Projects
            </li>
            <li
              class="px-4 py-3 rounded-xl flex items-center gap-3 text-[#40566e] font-medium cursor-pointer hover:bg-gray-100"
            >
              <i class="fa fa-tasks w-5"></i> Tasks
            </li>
            <li
              class="px-4 py-3 rounded-xl flex items-center gap-3 text-[#40566e] font-medium cursor-pointer hover:bg-gray-100"
            >
              <i class="fa fa-users w-5"></i> Users
            </li>
            <li
              class="px-4 py-3 rounded-xl flex items-center gap-3 text-[#40566e] font-medium cursor-pointer hover:bg-gray-100"
            >
              <i class="fa fa-cog w-5"></i> Settings
            </li>
          </ul>
        </nav>
      </aside>

      <!-- Main Content -->
      <main class="flex-1 bg-white p-8 overflow-y-auto flex flex-col gap-7">
        <!-- Stats Cards -->
        <div class="grid grid-cols-4 gap-5">
          <div
            class="bg-[#f9fcff] rounded-2xl p-5 border border-[#edf2f7] shadow-sm"
          >
            <div class="text-sm font-medium text-[#5a6f8c] tracking-wide">TOTAL TASKS</div>
            <div class="text-4xl font-bold text-[#0b1b33]">128</div>
          </div>
          <div class="bg-[#f9fcff] rounded-2xl p-5 border border-[#edf2f7] shadow-sm">
            <div class="text-sm font-medium text-[#5a6f8c] tracking-wide">IN PROGRESS</div>
            <div class="text-4xl font-bold text-[#0b1b33]">34</div>
          </div>
          <div class="bg-[#f9fcff] rounded-2xl p-5 border border-[#edf2f7] shadow-sm">
            <div class="text-sm font-medium text-[#5a6f8c] tracking-wide">COMPLETED</div>
            <div class="text-4xl font-bold text-[#0b1b33]">82</div>
          </div>
          <div class="bg-[#f9fcff] rounded-2xl p-5 border border-[#edf2f7] shadow-sm">
            <div class="text-sm font-medium text-[#5a6f8c] tracking-wide">OVERDUE</div>
            <div class="text-4xl font-bold text-[#0b1b33]">12</div>
          </div>
        </div>

        <!-- Charts Row -->
        <div class="grid grid-cols-2 gap-6">
          <!-- Task Distribution -->
          <div class="bg-[#f9fcff] rounded-2xl p-6 border border-[#edf2f7]">
            <div class="flex justify-between items-center mb-5">
              <span class="font-semibold text-[#1e293b]">Task Distribution</span>
              <a href="#" class="text-blue-500 text-xs font-medium">View All</a>
            </div>
            <div class="space-y-2">
              <div class="flex items-center gap-2 text-[#2d3f59]">
                <span class="w-3 h-3 rounded-full bg-orange-500"></span> To Do <span class="ml-auto font-bold">28</span>
              </div>
              <div class="flex items-center gap-2 text-[#2d3f59]">
                <span class="w-3 h-3 rounded-full bg-blue-500"></span> In Progress <span class="ml-auto font-bold">21</span>
              </div>
              <div class="flex items-center gap-2 text-[#2d3f59]">
                <span class="w-3 h-3 rounded-full bg-green-500"></span> Done <span class="ml-auto font-bold">14</span>
              </div>
            </div>
            <!-- mini bars -->
            <div class="flex items-end gap-3 mt-5 h-16">
              <div class="flex-1 bg-[#e6edf4] rounded-xl h-full flex items-end">
                <div class="w-full bg-orange-500 rounded-xl" style="height:28px"></div>
              </div>
              <div class="flex-1 bg-[#e6edf4] rounded-xl h-full flex items-end">
                <div class="w-full bg-blue-500 rounded-xl" style="height:21px"></div>
              </div>
              <div class="flex-1 bg-[#e6edf4] rounded-xl h-full flex items-end">
                <div class="w-full bg-green-500 rounded-xl" style="height:14px"></div>
              </div>
            </div>
          </div>

          <!-- Weekly Completion Rate -->
          <div class="bg-[#f9fcff] rounded-2xl p-6 border border-[#edf2f7]">
            <div class="font-semibold text-[#1e293b] mb-5">Weekly Completion Rate</div>
            <div class="flex justify-between text-[#6f85a2] text-xs font-medium px-1">
              <span>Mon</span><span>Tue</span><span>Wed</span><span>Thu</span><span>Fri</span><span>Sat</span><span>Sun</span>
            </div>
            <div class="flex items-end gap-1.5 h-36 mt-2">
              <div class="flex-1 bg-[#e2eaf3] rounded-2xl h-full flex items-end">
                <div class="w-full bg-gradient-to-t from-[#2f5fcf] to-[#4b7bec] rounded-2xl" style="height:42px"></div>
              </div>
              <div class="flex-1 bg-[#e2eaf3] rounded-2xl h-full flex items-end">
                <div class="w-full bg-gradient-to-t from-[#2f5fcf] to-[#4b7bec] rounded-2xl" style="height:28px"></div>
              </div>
              <div class="flex-1 bg-[#e2eaf3] rounded-2xl h-full flex items-end">
                <div class="w-full bg-gradient-to-t from-[#2f5fcf] to-[#4b7bec] rounded-2xl" style="height:56px"></div>
              </div>
              <div class="flex-1 bg-[#e2eaf3] rounded-2xl h-full flex items-end">
                <div class="w-full bg-gradient-to-t from-[#2f5fcf] to-[#4b7bec] rounded-2xl" style="height:34px"></div>
              </div>
              <div class="flex-1 bg-[#e2eaf3] rounded-2xl h-full flex items-end">
                <div class="w-full bg-gradient-to-t from-[#2f5fcf] to-[#4b7bec] rounded-2xl" style="height:62px"></div>
              </div>
              <div class="flex-1 bg-[#e2eaf3] rounded-2xl h-full flex items-end">
                <div class="w-full bg-gradient-to-t from-[#2f5fcf] to-[#4b7bec] rounded-2xl" style="height:25px"></div>
              </div>
              <div class="flex-1 bg-[#e2eaf3] rounded-2xl h-full flex items-end">
                <div class="w-full bg-gradient-to-t from-[#2f5fcf] to-[#4b7bec] rounded-2xl" style="height:47px"></div>
              </div>
            </div>
          </div>
        </div>

        <!-- Recent Activity -->
        <div class="bg-[#f9fcff] rounded-2xl p-6 border border-[#edf2f7]">
          <div class="flex justify-between items-center mb-3">
            <span class="font-semibold text-[#1e293b]">Recent Activity</span>
            <a href="#" class="text-blue-500 text-xs font-medium">View All</a>
          </div>
          <table class="w-full text-sm">
            <thead>
              <tr class="text-left text-[#50657e] text-xs font-semibold border-b border-[#dfe7ef]">
                <th class="pb-3">Task</th>
                <th class="pb-3">Action</th>
                <th class="pb-3">User</th>
                <th class="pb-3">Time</th>
                <th class="pb-3">Manager</th>
                <th class="pb-3">Collaborator</th>
              </tr>
            </thead>
            <tbody class="text-[#1e2b3e]">
              <tr class="border-b border-[#eaedf2]">
                <td class="py-4">Fix navigation bug on mobile</td>
                <td>
                  <span class="bg-blue-100 text-[#1d4ed8] px-3 py-1 rounded-full text-xs font-medium">Moved to Done</span>
                </td>
                <td>Sarah Chen</td>
                <td>2m ago</td>
                <td>Alex Morgan</td>
                <td class="text-[#b8c9dd]">—</td>
              </tr>
              <tr>
                <td class="py-4">Update API documentation</td>
                <td>
                  <span class="bg-purple-100 text-[#9333ea] px-3 py-1 rounded-full text-xs font-medium">Commented</span>
                </td>
                <td>Mike Ross</td>
                <td>15m ago</td>
                <td class="text-[#b8c9dd]">—</td>
                <td class="text-[#b8c9dd]">—</td>
              </tr>
            </tbody>
          </table>
          <div class="flex gap-6 text-xs text-[#a0b8d0] mt-4 pl-2">
            <span><i class="fa fa-user-tie mr-1"></i> Manager: Alex Morgan (on first task)</span>
            <span><i class="fa fa-user-friends mr-1"></i> Collaborator: —</span>
          </div>
        </div>
      </main>
    </div>
  </div>
</template>

<script setup lang="ts">
// Pour cette version statique, aucun code TS n'est nécessaire.
// Tu peux ajouter des données dynamiques ici plus tard.
</script>

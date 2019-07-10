from mpl_toolkits.mplot3d import Axes3D
from mpl_toolkits.mplot3d.art3d import Poly3DCollection
import numpy as np
import matplotlib.pyplot as plt


filename = 'C:/Users/aleksei/Desktop/Research/GIS/3D_models/separate_Shape 9659.obj'

counter = 0
vertices = []
vert_x = []
vert_y = []
vert_z = []
with open(filename) as f:
    for line in f:
        if line[0:2] == 'v ':

            asd = line[2:len(line)-1]
            vertex = np.asarray(asd.split(' '))
            vert_coord = vertex.astype(np.float)

            vertices.append(vert_coord)
            vert_x.append(vert_coord[0])
            vert_y.append(vert_coord[1])
            vert_z.append(vert_coord[2])
            counter = counter + 1

center = np.mean(vertices,0)
boards = 3*np.std(vertices,0)
print 'center = ', center, '; 3 * std = ', boards
print 'size of the array = (', np.size(vertices, 0), ', ', np.size(vertices, 1), ')'

#patches = np.array([36, 35, 38, 37])-1




fig = plt.figure()
ax = Axes3D(fig)
ax.set_xlim(center[0] - boards[0], center[0] + boards[0])
ax.set_ylim(center[2] - boards[2], center[2] + boards[2])
ax.set_zlim(center[1] - boards[1], center[1] + boards[1])
ax.autoscale_view()

with open(filename) as f:
    for line in f:
        if line[0:2] == 'f ':
            #asd = line[3:len(line) - 1]
            #vertex = np.asarray(asd.split(' '))
            #vert_coord = vertex.astype(np.float)

            qwe = line[2:len(line)-1]
            #print qwe, '; type = ', type(qwe), '; size = ', np.size(qwe)
            temp = np.asarray(qwe.split(' '))
            #print temp, '; type = ', type(temp), '; size = ', np.size(temp)
            patches = temp.astype(np.int) - 1
            verts = [list(zip([vert_x[i] for i in patches], [vert_z[i] for i in patches], [vert_y[i] for i in patches]))]
            ax.add_collection3d(Poly3DCollection(verts))


plt.show()
